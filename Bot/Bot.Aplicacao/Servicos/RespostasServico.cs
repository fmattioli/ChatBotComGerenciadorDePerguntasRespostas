using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Entidades;
using Bot.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class RespostasServico : IRespostasServico
    {
        private readonly IRespostasRepositorio respostasRepositorio;
        public RespostasServico(IRespostasRepositorio respostasRepositorio)
        {
            this.respostasRepositorio = respostasRepositorio;
        }

        public async Task<RetornoViewModel> AdicionarResposta(RespostasViewModel respostasViewModel)
        {
            var retorno = await respostasRepositorio.AdicionarResposta(new Resposta
            {
                Pergunta = new Perguntas
                {
                    Id = respostasViewModel.Pergunta.Id,
                },
                RespostaTexto = respostasViewModel.Resposta
            });

            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<RetornoViewModel> DesativarResposta(Guid Id)
        {
            var retorno = await respostasRepositorio.Desativar(Id, "Respostas");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<IEnumerable<RespostasViewModel>> ObterRespostas(Guid PerguntaId)
        {
            var respostas = new List<RespostasViewModel>();
            foreach (var item in await respostasRepositorio.ObterRespostas(PerguntaId))
            {
                respostas.Add(new RespostasViewModel
                {
                    Id = item.Id,
                    Pergunta = new PerguntasViewModel
                    {
                        Id = item.Pergunta.Id,
                        TextoPergunta = item.Pergunta.TextoPergunta,
                        Intencao = new IntencaoLuisViewModel
                        {
                            Id = item.Pergunta.Intencao.Id
                        }
                    },
                    Resposta = item.RespostaTexto,
                    Ativo = item.Ativo
                    
                });
            }

            return respostas.Where(a => a.Ativo);
        }

        public async Task<int> ObterTotalRespostas(Guid IntencaoId)
        {
            return await respostasRepositorio.ObterTotalRespostas(IntencaoId);
        }
    }
}
