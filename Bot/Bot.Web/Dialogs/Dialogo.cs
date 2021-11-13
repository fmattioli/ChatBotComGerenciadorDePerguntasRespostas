using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Web.Cards.Gerenciador;
using Bot.Web.CognitiveModels;
using Bot.Web.Dialogs.Comum;
using Bot.Web.Recognizers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Web.Dialogs
{
    public class Dialogo : CancelAndHelpDialog
    {
        private readonly GerenciarCards _gerenciadorCards;
        private readonly DialogoRecognizer _luisRecognizer;
        private readonly IComumServico _comumServico;
        private readonly ICardsServico _cardsServico;
        private readonly IIntencaoServico _intencoesServico;
        private readonly IMensagemInicioServico _mensagemInicioServico;

        public Dialogo(GerenciarCards gerenciadorCards, DialogoRecognizer luisRecognizer, IComumServico comumServico, ICardsServico cardsServico, IIntencaoServico intencoesServico, IMensagemInicioServico mensagemInicioServico)
           : base(nameof(Dialogo))
        {
            _luisRecognizer = luisRecognizer;
            _gerenciadorCards = gerenciadorCards;
            _comumServico = comumServico;
            _cardsServico = cardsServico;
            _intencoesServico = intencoesServico;
            _mensagemInicioServico = mensagemInicioServico;
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                ExibirMensagem,
                ProcessarMensagemRecebida
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> ExibirMensagem(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //Mensagem em caso de compreensão da mensagem enviado ao Bot (Card ou Mensagem).
            if (stepContext.Options is List<string> && !None)
            {
                foreach (var resposta in (IList<string>)stepContext.Options)
                    await DialogoComum.EnviarMensagem(stepContext, cancellationToken, $"{resposta}");

                var mensagemContinuarConversa = "Fique a vontade para me perguntar outras coisas...";
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(mensagemContinuarConversa, mensagemContinuarConversa, InputHints.ExpectingInput) }, cancellationToken);
            }

            //Mensagem em caso de não compreensão por parte do Bot.
            if (stepContext.Options is string && None)
            {
                await DialogoComum.EnviarMensagem(stepContext, cancellationToken, $"{stepContext.Options} \U0001F600");
                await DialogoComum.EnviarMensagem(stepContext, cancellationToken, "Vamos tentar novamente? Navegue entre os menus ou me envie uma mensagem.");
                return await stepContext.PromptAsync(nameof(TextPrompt), _gerenciadorCards.RetornarAdaptiveCard(await _cardsServico.ObterCards()), cancellationToken);
            }

            //Mensagem de inicio da conversa.
            EspecialidadeViewModel especialidade = stepContext.Options as EspecialidadeViewModel;
            var mensagens = await _mensagemInicioServico.ObterMensagensPorNome(especialidade.Nome);
            var ultimaMensagem = mensagens.Last();
            foreach (var mensagem in mensagens)
                if (mensagem.Id != ultimaMensagem.Id)
                    await DialogoComum.EnviarMensagem(stepContext, cancellationToken, mensagem.Mensagem);

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(ultimaMensagem.Mensagem, ultimaMensagem.Mensagem, InputHints.ExpectingInput) }, cancellationToken);
        }

        private async Task<DialogTurnResult> ProcessarMensagemRecebida(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            IList<string> respostas;
            if (await _comumServico.IsJson(stepContext.Result?.ToString()))
            {
                var intencaoViewModel = await _comumServico.DesserializarClasse(stepContext.Result.ToString());
                respostas = await _intencoesServico.ObterRespostaIntencaoCard(intencaoViewModel.Intencao);
                return await stepContext.ReplaceDialogAsync(InitialDialogId, respostas, cancellationToken);

            }

            var luisResult = await _luisRecognizer.RecognizeAsync<DialogoCognitiveModel>(stepContext.Context, cancellationToken);
            if (luisResult.Intencao.Contains("None"))
                return await stepContext.ReplaceDialogAsync(InitialDialogId, "Desculpa, não entendi o que você quis dizer... Mas processei sua pergunta e logo poderei responder ela. Obrigado por me ajudar a me tornar inteligente.", cancellationToken);

            return await stepContext.ReplaceDialogAsync(InitialDialogId, await _intencoesServico.ObterRespostaIntencaoLUIS(luisResult.Intencao), cancellationToken);
        }
    }
}
