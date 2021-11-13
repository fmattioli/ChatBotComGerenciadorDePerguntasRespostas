using System;

namespace Bot.Aplicacao.ViewModels
{
    public class IntencaoLuisViewModel
    {
        public Guid Id { get; set; }
        public string Intencao { get; set; }
        public EspecialidadeViewModel Especialidade { get; set; }
        public int TotalPerguntas { get; set; }
        public bool Ativo { get; set; }
    }
}
