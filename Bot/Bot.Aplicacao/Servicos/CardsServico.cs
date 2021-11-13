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
    public class CardsServico : ICardsServico
    {
        private readonly ICardsRepositorio cardsRepositorio;
        public CardsServico(ICardsRepositorio cardsRepositorio)
        {
            this.cardsRepositorio = cardsRepositorio;
        }
        public async Task<RetornoViewModel> Adicionar(CardsViewModel cardViewModel)
        {
            var card = new Cards
            {
                ConteudoJson = cardViewModel.ConteudoJson,
                ExibirInicio = cardViewModel.ExibirInicio,
                NomeCard = cardViewModel.NomeCard,
                Relevancia = cardViewModel.Relevancia
            };

            var retorno = await cardsRepositorio.Adicionar(card, "Cards");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
            
        }

        public async Task<IList<CardsViewModel>> ObterCards()
        {
            var listaCards = new List<CardsViewModel>();
            foreach(var item in await cardsRepositorio.ObterTodos("Cards"))
            {
                listaCards.Add(new CardsViewModel
                {
                    ConteudoJson = item.ConteudoJson,
                    ExibirInicio = item.ExibirInicio,
                    NomeCard = item.NomeCard,
                    Relevancia = item.Relevancia
                });
            }

            return listaCards.OrderBy(x => x.Relevancia).ToList();
        }
    }
}
