using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class IntencaoController : Controller
    {
        public async Task<IActionResult> Home([FromServices] IEspecialidadeServico especialidadeServico)
        {
            var especialidades = await especialidadeServico.ObterEspecialidades();
            return View(especialidades);
        }

        public async Task<IActionResult> Intencoes(string Id, [FromServices] IIntencaoServico intencoesServico, [FromServices] IMensagemInicioServico mensagemInicioServico)
        {
            var resultado = await mensagemInicioServico.ObterMensagens(Guid.Parse(Id));
            if (resultado.Any())
                return View(await intencoesServico.ObterTodasIntencoesLUIS(Guid.Parse(Id)));
            else
                return RedirectToAction("Home", "Especialidade");
        }

        public async Task<IActionResult> AdicionarIntencao([FromBody] IntencaoLuisViewModel intencaoLuisViewModel, [FromServices] IIntencaoServico intencoesServico)
        {
            var retorno = await intencoesServico.Adicionar(intencaoLuisViewModel);
            if (retorno.Sucesso)
                return Json(new { MsgErro = ""});

            return Json(new { MsgErro = retorno.MensagemErro });
        }
        
        public async Task<IActionResult> DesativarIntencao(Guid Id, [FromServices] IIntencaoServico intencoesServico)
        {
            var retorno = await intencoesServico.Desativar(Id);
            if (retorno.Sucesso)
                return Json(new { MsgErro = ""});

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
