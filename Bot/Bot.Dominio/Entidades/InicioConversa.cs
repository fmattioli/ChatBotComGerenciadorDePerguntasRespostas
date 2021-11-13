using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Dominio.Entidades
{
    public class InicioConversa
    {
        public Guid Id { get; set; }
        public string Mensagem { get; set; }
        public bool Ativo { get; set; }
        public int Relevancia { get; set; }
        public Especialidade Especialidade { get; set; }
        public Cards Card { get; set; }
    }
}
