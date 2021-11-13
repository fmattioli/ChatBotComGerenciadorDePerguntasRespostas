using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bot.Aplicacao.ViewModels
{
    public class EspecialidadeViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }

    public static class EspecialidadeViewModelTransicao
    {
        public static Guid Id { get; set; }
        public static string Nome { get; set; }
        public static string Descricao { get; set; }
        public static bool Ativo { get; set; }
    }

}
