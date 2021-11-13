using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.ViewModels
{
    public class PerguntasViewModel
    {
        public Guid Id { get; set; }
        public string TextoPergunta { get; set; }
        public IntencaoLuisViewModel Intencao { get; set; }
        public int TotalRespostas { get; set; }
        public bool Ativo { get; set; }
    }
}
