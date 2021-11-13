using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IMensagemInicioRepositorio : IBaseRepositorio<InicioConversa>
    {
        Task<IEnumerable<InicioConversa>> ObterMensagens(Guid EspecialidadeId);
        Task<IEnumerable<InicioConversa>> ObterMensagensPorNome(string Especialidade);
        Task<Retorno> AdicionarMensagem(InicioConversa inicioConversa);
    }
}
