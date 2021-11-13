using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.ViewModels
{
    public class RespostasViewModel
    {
        public Guid Id { get; set; }
        public string Resposta { get; set; }
        public PerguntasViewModel Pergunta { get; set; }
        public int TotalRespostas { get; set; }
        public bool Ativo { get; set; }
    }
}
