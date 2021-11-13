using System;

namespace Bot.Dominio.Entidades
{
    public class Intencao
    {
        public Guid Id { get; set; }
        public string IntencaoTexto { get; set; }
        public Especialidade Especialidade { get; set; }
        public bool Ativo { get; set; }
    }
}
