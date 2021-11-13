using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bot.Web.Gerenciador.Controllers
{
    public class UsuariosController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel usuario, [FromServices] IUsuarioServico usuarioServico)
        {
            if (await usuarioServico.ObterUsuario(usuario.Email, usuario.Senha) != null)
                return RedirectToAction("Home", "Home");

            ModelState.AddModelError("", "Usuario e/ou senha inválidas");
            return View(usuario);
        }
        public async Task<IActionResult> Cadastrar([FromServices] IUsuarioServico usuarioServico)
        {
            return View(await usuarioServico.ObterUsuarios());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioViewModel usuario, [FromServices] IUsuarioServico usuarioServico)
        {
            if (ModelState.IsValid)
            {
                var retorno = await usuarioServico.Criar(usuario);
                if (retorno.Sucesso)
                    return Json(new { MsgErro = "", HtmlGrid = retorno.Resultado });

                return Json(new { MsgErro = retorno.MensagemErro });
            }

            ModelState.AddModelError("", "Usuario e/ou senha inválidas");
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(string Id, [FromServices] IUsuarioServico usuarioServico)
        {
            var retorno = await usuarioServico.DesativarUsuario(Guid.Parse(Id));
            if (retorno.MensagemErro == "")
                return Json(new { MsgErro = string.Empty, HtmlGrid = retorno.Resultado });

            return Json(new { MsgErro = retorno.MensagemErro });
        }
    }
}
