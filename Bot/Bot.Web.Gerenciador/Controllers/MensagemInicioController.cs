using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class MensagemInicioController : Controller
    {
        public async Task<IActionResult> Mensagens(Guid Id, [FromServices] IMensagemInicioServico mensagemInicioServico)
        {
            var mensagens = await mensagemInicioServico.ObterMensagens(Id);
            ViewBag.EspecialidadeId = Id;
            ViewBag.Especialidade = mensagens.FirstOrDefault(a => a.Especialidade.Id == Id)?.Especialidade.Nome;
            return View(mensagens.OrderBy(r => r.Relevancia));
        }

        public async Task<IActionResult> AdicionarMensagem([FromBody] InicioConversaViewModel mensagemInicio, [FromServices] IMensagemInicioServico mensagemInicioServico)
        {
            var retorno = await mensagemInicioServico.AdicionarMensagem(mensagemInicio);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }

        public async Task<IActionResult> DesativarMensagem(Guid Id, [FromServices] IMensagemInicioServico mensagemInicioServico)
        {
            var retorno = await mensagemInicioServico.Desativar(Id);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
