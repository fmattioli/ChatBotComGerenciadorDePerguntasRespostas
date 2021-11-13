using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class RespostaController : Controller
    {
        public async Task<IActionResult> Respostas(Guid Id, [FromServices] IRespostasServico respostasServico)
        {
            ViewBag.PerguntaId = Id;
            var respostas = await respostasServico.ObterRespostas(Id);
            ViewBag.PerguntaTexto = respostas.FirstOrDefault(a=> a.Pergunta.Id == Id)?.Pergunta.TextoPergunta;
            ViewBag.IntencaoId = respostas.FirstOrDefault(a=> a.Pergunta.Id == Id)?.Pergunta.Intencao.Id;
            return View(respostas);
        }

        public async Task<IActionResult> AdicionarResposta([FromBody] RespostasViewModel respostasViewModel, [FromServices] IRespostasServico respostasServico)
        {
            var retorno = await respostasServico.AdicionarResposta(respostasViewModel);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }

        public async Task<IActionResult> DesativarResposta(Guid Id, [FromServices] IRespostasServico respostasServico)
        {
            var retorno = await respostasServico.DesativarResposta(Id);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
