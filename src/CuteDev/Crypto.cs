/* Author: Volkan ŞENDAĞ - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CuteDev
{
    /// <summary>
    /// Şifreleme işlemlerini yapar (volkansendag - 2013.05.24)
    /// </summary>
    public class Crypto
    {
        #region Global

        DESProvider crypto;

        #endregion

        #region Constructor

        public Crypto()
        {
            crypto = new DESProvider(Config.Guid);
        }

        public Crypto(string key)
        {
            crypto = new DESProvider(key);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Metni şifreler (volkansendag - 2013.05.24)
        /// </summary>
        public string Encrypt(string val)
        {
            return crypto.Encrypt(val);
        }

        /// <summary>
        /// Metni şifrelemeyi dener. Şifreleyemezse val degerini geri dondurur. (volkansendag - 2013.05.24)
        /// </summary>
        public string TryEncrypt(string val)
        {
            try
            {
                return crypto.Encrypt(val);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Şifreli metni çözer (volkansendag - 2013.05.24)
        /// </summary>
        public string Decrypt(string val)
        {
            return crypto.Decrypt(val);
        }

        /// <summary>
        /// Şifreli metni çözermeyi dener. Çözemezse val degerini döndürür (volkansendag - 2013.05.24)
        /// </summary>
        public string TryDecrypt(string val)
        {
            try
            {
                return crypto.Decrypt(val);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
