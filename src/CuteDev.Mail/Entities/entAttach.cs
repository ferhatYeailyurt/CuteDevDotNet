using System;
using System.Collections.Generic;
using System.Web;

namespace CuteDev.Mail.Entities
{
    /// <summary>
    /// E-posta ek dosya varlığı (volkansendag - 2014.12.04)
    /// </summary>
    [Serializable]
    public class entAttach
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}