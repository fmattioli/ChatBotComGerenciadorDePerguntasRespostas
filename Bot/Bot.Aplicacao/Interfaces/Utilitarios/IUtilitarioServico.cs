using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface IUtilitarioServico<T>
    {
        Task<T> DesserializarClasse(string Json);
        /// <summary>
        /// Dentro de uma conversação podemos receber uma mensagem ou um retorno de um submit, este método faz a validação do conteúdo recebido na conversa.
        /// </summary>
        /// <param name="conteudo"></param>
        /// <returns></returns>
        Task<bool> IsJson(string conteudo);
    }
}
