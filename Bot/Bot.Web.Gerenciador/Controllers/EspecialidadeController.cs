using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class EspecialidadeController : Controller
    {
        public async Task<IActionResult> Home([FromServices] IEspecialidadeServico especialidadeServico)
        {
            var especialidades = await especialidadeServico.ObterEspecialidades();
            return View(especialidades);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEspecialidade([FromBody] EspecialidadeViewModel especialidade, [FromServices] IEspecialidadeServico especialidadeServico)
        {
            var retorno = await especialidadeServico.Adicionar(especialidade);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "", HtmlGrid = retorno.Resultado });

            return Json(new { MsgErro = retorno.MensagemErro });
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirEspecialidade(string Id, [FromServices] IEspecialidadeServico especialidadeServico)
        {
            var retorno = await especialidadeServico.DesativarEspecialidade(Guid.Parse(Id));
            if (retorno.MensagemErro == "")
                return Json(new { MsgErro = string.Empty, HtmlGrid = retorno.Resultado });

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
