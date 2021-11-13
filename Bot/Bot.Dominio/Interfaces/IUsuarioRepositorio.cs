using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        public Task<Usuario> ObterUsuario(string Email, string Senha);
    }
}
