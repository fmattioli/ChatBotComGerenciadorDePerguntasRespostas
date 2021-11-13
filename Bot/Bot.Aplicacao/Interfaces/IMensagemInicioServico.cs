using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IMensagemInicioServico
    {
        Task<IEnumerable<InicioConversaViewModel>> ObterMensagens(Guid EspecialidadeId);
        Task<IEnumerable<InicioConversaViewModel>> ObterMensagensPorNome(string Especialidade);
        Task<RetornoViewModel> AdicionarMensagem(InicioConversaViewModel inicioConversaViewModel);
        Task<RetornoViewModel> Desativar(Guid Id);
    }
}
