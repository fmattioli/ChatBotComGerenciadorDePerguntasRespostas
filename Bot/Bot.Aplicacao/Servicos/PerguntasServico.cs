using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Entidades;
using Bot.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class PerguntasServico : IPerguntasServico
    {
        private readonly IPerguntasRepositorio perguntasRepositorio;
        private readonly IRespostasServico respostasServico;
        public PerguntasServico(IPerguntasRepositorio perguntasRepositorio, IRespostasServico respostasServico)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.respostasServico = respostasServico;
        }

        public async Task<RetornoViewModel> Adicionar(PerguntasViewModel perguntasViewModel)
        {
            var pergunta = new Perguntas
            {
                TextoPergunta = perguntasViewModel.TextoPergunta,
                Intencao = new Intencao
                {
                    Id = perguntasViewModel.Intencao.Id
                }
            };

            var retorno = await perguntasRepositorio.AdicionarPergunta(pergunta);
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<RetornoViewModel> Desativar(Guid Id)
        {
            var retorno = await perguntasRepositorio.Desativar(Id, "Perguntas");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<IEnumerable<PerguntasViewModel>> ObterPerguntas(Guid IntencaoId)
        {
            var perguntas = new List<PerguntasViewModel>();
            foreach (var item in await perguntasRepositorio.ObterPerguntas(IntencaoId))
            {
                perguntas.Add(new PerguntasViewModel
                {
                    Id = item.Id,
                    Intencao = new IntencaoLuisViewModel
                    {
                        Id = item.Intencao.Id,
                        Intencao = item.Intencao.IntencaoTexto,
                        Especialidade = new EspecialidadeViewModel
                        {
                            Id = item.Intencao.Especialidade.Id
                        }
                    },
                    Ativo = item.Ativo,
                    TextoPergunta = item.TextoPergunta,
                    TotalRespostas = await respostasServico.ObterTotalRespostas(item.Intencao.Id)
                });
            }

            return perguntas.Where(a => a.Ativo);
        }

        public async Task<int> ObterTotalPerguntas(Guid IntencaoId)
        {
            return await perguntasRepositorio.ObterTotalPerguntas(IntencaoId);
        }
    }
}
