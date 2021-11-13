using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IBaseRepositorio<T> where T : class
    {
        Task<Retorno> Adicionar(T entidade, string Tabela);
        Task<Retorno> Excluir(Guid Id, string Tabela);
        Task<Retorno> Desativar(Guid Id, string Tabela);
        Task<T> ObterPorId(string Tabela, Guid Id);
        Task<IList<T>> ObterTodos(string Tabela);
        Task VerificarStatusBancoDeDados(string ConectionString);
    }
}
