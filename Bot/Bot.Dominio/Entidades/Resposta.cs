using System;

namespace Bot.Dominio.Entidades
{
    public class Resposta
    {

        public Guid Id { get; set; }
        public string RespostaTexto { get; set; }
        public Perguntas Pergunta { get; set; }
        public int TotalRespostas { get; set; }
        public bool Ativo { get; set; }
    }
}
