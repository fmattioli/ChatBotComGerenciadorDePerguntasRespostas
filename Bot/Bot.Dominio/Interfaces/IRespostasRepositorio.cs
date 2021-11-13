using Bot.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Interfaces
{
    public interface IRespostasRepositorio : IBaseRepositorio<Resposta>
    {
        Task<Retorno> AdicionarResposta(Resposta resposta);
        Task<int> ObterTotalRespostas(Guid IntencaoId);
        Task<IEnumerable<Resposta>> ObterRespostas(Guid PerguntaId);

    }
}
