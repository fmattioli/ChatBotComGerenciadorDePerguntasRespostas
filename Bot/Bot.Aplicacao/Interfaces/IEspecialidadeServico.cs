using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IEspecialidadeServico
    {
        Task<RetornoViewModel> Adicionar(EspecialidadeViewModel especialidadeViewModel);
        Task<RetornoViewModel> DesativarEspecialidade(Guid Id);
        Task<IList<EspecialidadeViewModel>> ObterEspecialidades();
        Task<EspecialidadeViewModel> ObterEspecialidadePorId(Guid Id);
        Task<EspecialidadeViewModel> ObterEspecialidadePorNome(string Nome);
        Task<string> MontarHtmlGrid();
     
    }
}
