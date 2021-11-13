using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.ViewModels;
using Bot.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Dominio.Entidades;


namespace Bot.Aplicacao.Servicos
{
    public class EspecialidadeServico : IEspecialidadeServico
    {
        private readonly IEspecialidadeRepositorio especialidadeRepositorio;
        private readonly IIntencoesRepositorio intencaoRepositorio;
        public EspecialidadeServico(IEspecialidadeRepositorio especialidadeRepositorio, IIntencoesRepositorio intencaoRepositorio)
        {
            this.especialidadeRepositorio = especialidadeRepositorio;
            this.intencaoRepositorio = intencaoRepositorio;
        }

        /// <summary>
        /// Método responsável por inserir uma especialidade de bot e tbm a sua primeira intenção default.
        /// </summary>
        /// <param name="especialidadeViewModel"></param>
        /// <returns></returns>
        public async Task<RetornoViewModel> Adicionar(EspecialidadeViewModel especialidadeViewModel)
        {
            var especialidade = new Especialidade()
            {
                Descricao = especialidadeViewModel.Descricao,
                Nome = especialidadeViewModel.Nome,
                Ativo = true
            };

            var retorno = await especialidadeRepositorio.Adicionar(especialidade, "Especialidades");

            //Inserir uma intenção default para erros em todas as especialidades.
            var intencao = new Intencao
            {
                Especialidade = await especialidadeRepositorio.ObterEspecialidadePorNome(especialidadeViewModel.Nome),
                IntencaoTexto = "None"
            };

            await intencaoRepositorio.AdicionarIntencao(intencao);

            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso,
                Resultado = await MontarHtmlGrid()
            };
        }

        public async Task<RetornoViewModel> DesativarEspecialidade(Guid Id)
        {
            var retorno = await especialidadeRepositorio.Desativar(Id, "Especialidades");
            return new RetornoViewModel
            {
                MensagemErro = retorno.MensagemErro,
                Sucesso = retorno.Sucesso,
                Resultado = await MontarHtmlGrid()
            };
        }


        public async Task<EspecialidadeViewModel> ObterEspecialidadePorId(Guid Id)
        {
            var retorno = await especialidadeRepositorio.ObterPorId("Especialidades",Id);
            return new EspecialidadeViewModel
            {
                Nome = retorno.Nome
            };

        }

        public async Task<EspecialidadeViewModel> ObterEspecialidadePorNome(string Nome)
        {
            var retorno = await especialidadeRepositorio.ObterEspecialidadePorNome(Nome);
            return new EspecialidadeViewModel
            {
                Nome = retorno.Nome
            };
        }

        public async Task<IList<EspecialidadeViewModel>> ObterEspecialidades()
        {
            var especialidades = new List<EspecialidadeViewModel>();
            foreach (var item in await especialidadeRepositorio.ObterTodos("Especialidades"))
                especialidades.Add(new EspecialidadeViewModel
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Nome = item.Nome,
                    Ativo = item.Ativo
                });

            return especialidades.Where(e => e.Ativo).ToList();
        }


        public async Task<string> MontarHtmlGrid()
        {
            var builder = new StringBuilder();
            foreach (var item in await ObterEspecialidades())
            {
                builder.AppendLine("<tr>");
                builder.AppendLine("<td>");
                builder.AppendLine($"{item.Nome}");
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine($"{item.Descricao}");
                builder.AppendLine("</td>");
                builder.AppendLine("<td>");
                builder.AppendLine($"<a class=\"btn-floating btn-large waves-effect waves-light black\"><i class=\"material-icons\" onclick=\"excluirEspecialidade('{item.Id}')\">delete</i></a>");
                builder.AppendLine("</td>");
                builder.AppendLine("</tr>");
            }
            return builder.ToString();
        }


    }
}
