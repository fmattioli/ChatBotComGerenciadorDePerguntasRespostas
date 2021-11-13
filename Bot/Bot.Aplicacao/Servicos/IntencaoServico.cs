using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Interfaces;
using Bot.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Bot.Aplicacao.Servicos
{
    public class IntencaoServico : IIntencaoServico
    {
        private readonly IIntencoesRepositorio intencoesRepositorio;
        private readonly IPerguntasServico perguntasServico;
        public IntencaoServico(IIntencoesRepositorio intencoesRepositorio, IPerguntasServico perguntasServico)
        {
            this.intencoesRepositorio = intencoesRepositorio;
            this.perguntasServico = perguntasServico;
        }

        public async Task<RetornoViewModel> Adicionar(IntencaoLuisViewModel intencoesViewModel)
        {
            var intencao = new Intencao
            {
                IntencaoTexto = intencoesViewModel.Intencao,
                Especialidade = new Especialidade
                {
                    Id = intencoesViewModel.Especialidade.Id
                }
            };

            var retorno = await intencoesRepositorio.AdicionarIntencao(intencao);
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<IList<string>> ObterRespostaIntencaoLUIS(string Intencao)
        {
            var respostas = new List<string>();
            foreach (var item in await intencoesRepositorio.ObterRespostasIntencaoLUIS(Intencao))
                respostas.Add(item);
            return respostas;
        }


        public async Task<IList<string>> ObterRespostaIntencaoCard(string Intencao)
        {
            var respostas = new List<string>();
            foreach (var item in await intencoesRepositorio.ObterRespostasIntencaoCard(Intencao))
                respostas.Add(item);
            return respostas;
        }

        public async Task<IList<IntencaoLuisViewModel>> ObterTodasIntencoesLUIS(Guid EspecialidadeId)
        {
            var intencoes = new List<IntencaoLuisViewModel>();
            foreach (var item in await intencoesRepositorio.ObterTodasIntencoes(EspecialidadeId))
            {
                intencoes.Add(new IntencaoLuisViewModel
                {
                    Especialidade = new EspecialidadeViewModel
                    {
                        Id = item.Especialidade.Id,
                        Ativo = item.Especialidade.Ativo,
                        Descricao = item.Especialidade.Descricao,
                        Nome = item.Especialidade.Nome
                    },
                    Id = item.Id,
                    Intencao = item.IntencaoTexto == "None" ? "Erro padrão" : item.IntencaoTexto,
                    Ativo = item.Ativo,
                    TotalPerguntas = await perguntasServico.ObterTotalPerguntas(item.Id)
                }); 
            }

            return intencoes.Where(a => a.Ativo).ToList();
        }

        public async Task<RetornoViewModel> Desativar(Guid Id)
        {
            var retorno = await intencoesRepositorio.Desativar(Id, "Intencoes");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Resultado = "",
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<IntencaoLuisViewModel> Obter(Guid Id)
        {
            var retorno = await intencoesRepositorio.ObterPorId("Intencoes", Id);
            return new IntencaoLuisViewModel
            {
                Ativo = retorno.Ativo,
                Intencao = retorno.IntencaoTexto
            };
        }
    }
}
