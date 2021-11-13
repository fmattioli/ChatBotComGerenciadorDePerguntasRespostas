using Bot.Aplicacao.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class UtilitarioServico<T> : IUtilitarioServico<T> where T : class
    {
        public async Task<T> DesserializarClasse(string Json)
        {
            T classeConcreta = null;
            await Task.Run(() =>
            {
                classeConcreta = JsonConvert.DeserializeObject<T>(Json);
            });
            return classeConcreta;
        }

        public async Task<bool> IsJson(string conteudo)
        {
            bool isJson = false;
            await Task.Run(() =>
            {
                isJson = conteudo.StartsWith('{') && conteudo.EndsWith('}');
            });
            return isJson;
        }
    }
}
