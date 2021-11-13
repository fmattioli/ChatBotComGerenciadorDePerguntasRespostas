using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Entidades
{
    public class Cards
    {
        public Guid Id { get; set; }
        public string ConteudoJson { get; set; }
        public string NomeCard { get; set; }
        public bool ExibirInicio { get; set; }
        public short Relevancia { get; set; }
    }
}
