using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Entidades;
using Bot.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ICriptografiaServico criptografia;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ICriptografiaServico criptografia)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.criptografia = criptografia;
        }
        public async Task<RetornoViewModel> Criar(UsuarioViewModel usuario)
        {
            var usuarioDominio = new Usuario
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Ativo = true,
                Senha = usuario.Senha
            };

            var retorno = await usuarioRepositorio.Adicionar(usuarioDominio, "Usuarios");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso,
                Resultado = await MontarHtmlGrid()
            };
        }

        public async Task<RetornoViewModel> DesativarUsuario(Guid Id)
        {
            var retorno = await usuarioRepositorio.Desativar(Id, "Usuarios");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso,
                Resultado = await MontarHtmlGrid()
            };
        }

        public async Task<string> MontarHtmlGrid()
        {
            var builder = new StringBuilder();
            foreach (var item in await ObterUsuarios())
            {
                builder.AppendLine("<tr>");
                builder.AppendLine("<td>");
                builder.AppendLine($"{item.Nome}");
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine($"{item.Email}");
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine($"<a class=\"btn-floating btn-large waves-effect waves-light red\"><i class=\"material-icons\" onclick=\"excluirUsuario('{item.Id}')\">delete</i></a>");
                builder.AppendLine("</td>");
                builder.AppendLine("</tr>");
            }
            return builder.ToString();
        }

        public async Task<UsuarioViewModel> ObterUsuario(string Email, string Senha)
        {
            var usuario = await usuarioRepositorio.ObterUsuario(Email, await criptografia.Encrypt(Senha));
            usuario = new Usuario
            {
                Ativo = true,
                Email = "felipe@felipe.com",
                Nome = "Felipe",
                Id = new Guid(),
                Senha = "24332433ab"
            };
            if (usuario is not null)
                return new UsuarioViewModel
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Id = usuario.Id
                };
            return null;
        }

        public async Task<IList<UsuarioViewModel>> ObterUsuarios()
        {
            var usuarios = new List<UsuarioViewModel>();
            foreach (var item in await usuarioRepositorio.ObterTodos("Usuarios"))
                usuarios.Add(new UsuarioViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Ativo = item.Ativo,
                    Email = item.Email
                });

            return usuarios.Where(e => e.Ativo).ToList();
        }
    }
}
