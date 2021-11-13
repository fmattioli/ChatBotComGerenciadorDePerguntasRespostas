using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface ICardsServico
    {
        Task<RetornoViewModel> Adicionar(CardsViewModel cardViewModel);
        Task<IList<CardsViewModel>> ObterCards();
    }
}
