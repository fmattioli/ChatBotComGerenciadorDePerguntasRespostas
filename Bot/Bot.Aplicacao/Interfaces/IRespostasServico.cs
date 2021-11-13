using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IRespostasServico
    {
        Task<RetornoViewModel> AdicionarResposta(RespostasViewModel respostasViewModel);
        Task<RetornoViewModel> DesativarResposta(Guid Id);
        Task<int> ObterTotalRespostas(Guid IntencaoId);
        Task<IEnumerable<RespostasViewModel>> ObterRespostas(Guid PerguntaId);

    }
}
