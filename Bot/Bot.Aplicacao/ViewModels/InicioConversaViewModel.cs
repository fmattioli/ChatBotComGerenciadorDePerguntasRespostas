using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.ViewModels
{
    public class InicioConversaViewModel
    {
        public Guid Id { get; set; }
        public string Mensagem { get; set; }
        public bool Ativo { get; set; }
        public int Relevancia { get; set; }
        public EspecialidadeViewModel Especialidade { get; set; }
        public CardsViewModel Card { get; set; }
    }
}
