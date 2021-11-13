using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.ViewModels
{
    public class CardsViewModel
    {
        public Guid Id { get; set; }
        public string ConteudoJson { get; set; }
        public string NomeCard { get; set; }
        public bool ExibirInicio { get; set; }
        public short Relevancia { get; set; }
        
    }
}
