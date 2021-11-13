using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IPerguntasRepositorio : IBaseRepositorio<Perguntas>
    {
        Task<int> ObterTotalPerguntas(Guid IntencaoId);
        Task<IEnumerable<Perguntas>> ObterPerguntas(Guid IntencaoId);
        Task<Retorno> AdicionarPergunta(Perguntas Pergunta);
    }
}
