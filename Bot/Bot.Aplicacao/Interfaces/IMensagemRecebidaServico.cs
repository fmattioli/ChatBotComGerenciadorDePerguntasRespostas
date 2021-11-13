using Bot.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IMensagemRecebidaServico
    {
        Task<RetornoViewModel> Adicionar(MensagemViewModel mensagemViewModel);
    }
}
