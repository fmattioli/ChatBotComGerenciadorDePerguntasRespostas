using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Interfaces
{
    public interface ICriptografiaServico
    {
        public Task<string> Encrypt(string textToEncrypt);
        public Task<string> Decripty(string encryptedText);
        public Task SetBeginCryptography();
        public Task SetOperation();
    }
}
