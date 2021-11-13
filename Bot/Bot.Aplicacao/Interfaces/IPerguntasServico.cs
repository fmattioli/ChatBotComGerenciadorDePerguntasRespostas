using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IPerguntasServico
    {
        Task<int> ObterTotalPerguntas(Guid IntencaoId);
        Task<RetornoViewModel> Adicionar(PerguntasViewModel perguntasViewModel);
        Task<RetornoViewModel> Desativar(Guid Id);
        Task<IEnumerable<PerguntasViewModel>> ObterPerguntas(Guid IntencaoId);
    }
}
