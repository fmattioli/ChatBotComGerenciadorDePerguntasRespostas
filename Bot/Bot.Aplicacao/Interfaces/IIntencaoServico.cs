using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IIntencaoServico
    {
        Task<RetornoViewModel> Adicionar(IntencaoLuisViewModel intencoesViewModel);
        Task<RetornoViewModel> Desativar(Guid Id);
        Task<IntencaoLuisViewModel> Obter(Guid Id);
        Task<IList<string>> ObterRespostaIntencaoLUIS(string Intencao);
        Task<IList<string>> ObterRespostaIntencaoCard(string Intencao);
        Task<IList<IntencaoLuisViewModel>> ObterTodasIntencoesLUIS(Guid EspecialidadeId);


    }
}
