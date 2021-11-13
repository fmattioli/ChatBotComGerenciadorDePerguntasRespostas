using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Entidades
{
    public class Perguntas
    {
        public Guid Id { get; set; }
        public string TextoPergunta { get; set; }
        public Intencao Intencao { get; set; }
        public bool Ativo { get; set; }
    }
}
