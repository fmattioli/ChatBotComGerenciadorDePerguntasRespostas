using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IUsuarioServico
    {
        Task<IList<UsuarioViewModel>> ObterUsuarios();
        Task<UsuarioViewModel> ObterUsuario(string Email, string Senha);
        Task<string> MontarHtmlGrid();
        Task<RetornoViewModel> Criar(UsuarioViewModel usuario);
        Task<RetornoViewModel> DesativarUsuario(Guid Id);
    }
}
