using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class PerguntaController : Controller
    {
        public async Task<IActionResult> Perguntas(Guid Id, [FromServices] IPerguntasServico perguntasServico, [FromServices] IIntencaoServico intencoesServico)
        {
            var intencao = await intencoesServico.Obter(Id);
            ViewBag.IntencaoId = Id;
            ViewBag.Intencao = intencao.Intencao;
            return View(await perguntasServico.ObterPerguntas(Id));
        }

        public async Task<IActionResult> AdicionarPergunta([FromBody] PerguntasViewModel perguntaViewModel, [FromServices] IPerguntasServico perguntaServico)
        {
            var retorno = await perguntaServico.Adicionar(perguntaViewModel);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }

        public async Task<IActionResult> DesativarPergunta(Guid Id, [FromServices] IPerguntasServico perguntaServico)
        {
            var retorno = await perguntaServico.Desativar(Id);
            if (retorno.Sucesso)
                return Json(new { MsgErro = "" });

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
