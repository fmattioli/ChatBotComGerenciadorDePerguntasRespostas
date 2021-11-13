using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class MensagemRecebidaServico : IMensagemRecebidaServico
    {
        private readonly IMensagemRecebidaRepositorio mensagemRecebidaRepositorio;
        public MensagemRecebidaServico(IMensagemRecebidaRepositorio mensagemRecebidaRepositorio)
        {
            this.mensagemRecebidaRepositorio = mensagemRecebidaRepositorio;
        }
        public async Task<RetornoViewModel> Adicionar(MensagemViewModel mensagemViewModel)
        {

            var retorno = await mensagemRecebidaRepositorio.Adicionar(new Dominio.Entidades.Mensagem
            {
                MensagemRecebida = mensagemViewModel.MensagemRecebida
            }, "Mensagens");

            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso
            };
        }
    }
}
