using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IIntencoesRepositorio : IBaseRepositorio<Intencao>
    {
        public Task<IList<string>> ObterRespostasIntencaoLUIS(string Intencao);
        public Task<IList<string>> ObterRespostasIntencaoCard(string Intencao);
        public Task<IEnumerable<Intencao>> ObterTodasIntencoes(Guid EspecialidadeId);
        Task<Retorno> AdicionarIntencao(Intencao Intencao);
    }
}
