using System;
using System.Collections.Generic;
using System.Web;

namespace CuteDev.Mail.Results
{
    /// <summary>
    /// Temel servis sonuç sınıfı (volkansendag - 2014.12.04)
    /// </summary>
    [Serializable]
    public class rCore
    {
        public bool Error { get; set; }

        public string Message { get; set; }
    }
}