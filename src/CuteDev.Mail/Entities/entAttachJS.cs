using System;
using System.Collections.Generic;
using System.Web;

namespace CuteDev.Mail.Entities
{
    /// <summary>
    /// E-posta ek dosya varlığı (javascript için) (volkansendag - 2014.12.04)
    /// </summary>
    [Serializable]
    public class entAttachJS
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}