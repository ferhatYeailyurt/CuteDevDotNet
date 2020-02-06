using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CuteDev
{

    internal class DESProvider
    {
        #region Globals

        TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

        #endregion

        #region Constructor

        public DESProvider(string key)
        {
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);

            this.plusKey = "plsky";
        }

        #endregion

        #region Functions

        private byte[] TruncateHash(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            Array.Resize(ref hash, length);

            return hash;
        }

        public string Encrypt(string text, bool replacePlusKeys = true)
        {
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(text);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            var encText = Convert.ToBase64String(ms.ToArray());


            if (replacePlusKeys && !string.IsNullOrEmpty(this.plusKey))
            {
                encText = encText.Replace("+", this.plusKey);
            }

            return encText;
        }

        private string plusKey { get; set; }

        public string Decrypt(string encryptedText, bool replacePlusKeys = true)
        {
            try
            {
                if (replacePlusKeys && !string.IsNullOrEmpty(this.plusKey))
                {
                    encryptedText = encryptedText.Replace(this.plusKey, "+");
                }

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                return System.Text.Encoding.Unicode.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Şifrelenmiş veri okunurken hata oluştu!" + ex.Message);
            }
        }

        #endregion
    }
}
