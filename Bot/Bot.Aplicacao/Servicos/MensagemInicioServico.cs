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
    public class MensagemInicioServico : IMensagemInicioServico
    {
        private readonly IMensagemInicioRepositorio mensagemInicioRepositorio;
        public MensagemInicioServico(IMensagemInicioRepositorio mensagemInicioRepositorio)
        {
            this.mensagemInicioRepositorio = mensagemInicioRepositorio;
        }
        public async Task<RetornoViewModel> AdicionarMensagem(InicioConversaViewModel inicioConversaViewModel)
        {
            var retorno = await mensagemInicioRepositorio.AdicionarMensagem(new InicioConversa
            {
                Mensagem = inicioConversaViewModel.Mensagem,
                Card = null,
                Especialidade = new Especialidade
                {
                    Id = inicioConversaViewModel.Especialidade.Id
                }
            });

            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<RetornoViewModel> Desativar(Guid Id)
        {
            var retorno = await mensagemInicioRepositorio.Desativar(Id, "InicioConversa");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }

        public async Task<IEnumerable<InicioConversaViewModel>> ObterMensagens(Guid EspecialidadeId)
        {
            var mensagens = new List<InicioConversaViewModel>();
            foreach (var item in await mensagemInicioRepositorio.ObterMensagens(EspecialidadeId))
            {
                mensagens.Add(new InicioConversaViewModel
                {
                    Especialidade = new EspecialidadeViewModel
                    {
                        Id = item.Especialidade.Id,
                        Ativo = item.Especialidade.Ativo,
                        Descricao = item.Especialidade.Descricao,
                        Nome = item.Especialidade.Nome
                    },
                    Id = item.Id,

                    Card = item.Card != null ? new CardsViewModel
                    {
                        NomeCard = item.Card.NomeCard,
                        ExibirInicio = item.Card.ExibirInicio,
                        Relevancia = item.Card.Relevancia
                    } : null,
                    Ativo = item.Ativo,
                    Mensagem = item.Mensagem,
                    Relevancia = item.Relevancia

                });
            }

            return mensagens.Where(a => a.Ativo).ToList();
        }

        public async Task<IEnumerable<InicioConversaViewModel>> ObterMensagensPorNome(string Especialidade)
        {
            var mensagens = new List<InicioConversaViewModel>();
            foreach (var item in await mensagemInicioRepositorio.ObterMensagensPorNome(Especialidade))
            {
                mensagens.Add(new InicioConversaViewModel
                {
                    Especialidade = new EspecialidadeViewModel
                    {
                        Id = item.Especialidade.Id,
                        Ativo = item.Especialidade.Ativo,
                        Descricao = item.Especialidade.Descricao,
                        Nome = item.Especialidade.Nome
                    },
                    Id = item.Id,

                    Card = item.Card != null ? new CardsViewModel
                    {
                        NomeCard = item.Card.NomeCard,
                        ExibirInicio = item.Card.ExibirInicio,
                        Relevancia = item.Card.Relevancia
                    } : null,
                    Ativo = item.Ativo,
                    Mensagem = item.Mensagem,
                    Relevancia = item.Relevancia

                });
            }

            return mensagens.Where(a => a.Ativo).OrderBy(a => a.Relevancia);
        }
    }
}
