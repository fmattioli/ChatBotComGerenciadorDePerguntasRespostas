using Bot.Aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Aplicacao.Servicos
{
    public class CriptografiaService : ICriptografiaServico
    {
        protected RijndaelManaged rijndael { get; set; } = new RijndaelManaged();
        public byte[] passBytes { get; set; }
        public byte[] encryptionkeyBytes { get; set; }

        public async Task<string> Decripty(string encryptedText)
        {
            await SetOperation();

            byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);
            encryptionkeyBytes = new byte[0x10];

            await SetBeginCryptography();

            byte[] textByte = rijndael.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(textByte);
        }

        public async Task<string> Encrypt(string textToEncrypt)
        {
            string encryptionKey = "felipemattiolidossantos";
            passBytes = Encoding.UTF8.GetBytes(encryptionKey);
            await SetOperation();
            await SetBeginCryptography();

            byte[] textDataByte = Encoding.UTF8.GetBytes(textToEncrypt);
            ICryptoTransform objtransform = rijndael.CreateEncryptor();
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public async Task SetBeginCryptography()
        {
            await Task.Run(() =>
            {
                encryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                int len = passBytes.Length;
                if (len > encryptionkeyBytes.Length)
                {
                    len = encryptionkeyBytes.Length;
                }

                Array.Copy(passBytes, encryptionkeyBytes, len);

                rijndael.Key = encryptionkeyBytes;
                rijndael.IV = encryptionkeyBytes;
            });
        }

        public async Task SetOperation()
        {
            await Task.Run(() =>
            {
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.KeySize = 0x80;
                rijndael.BlockSize = 0x80;
            });
        }
    }
}
