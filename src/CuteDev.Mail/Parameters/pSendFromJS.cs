using CuteDev.Mail.Entities;
using System;
using System.Collections.Generic;
using System.Web;

namespace CuteDev.Mail.Parameters
{
    /// <summary>
    /// E-posta gönderme parametresi (javascript için) (volkansendag - 2014.12.04)
    /// </summary>
    [Serializable]
    public class pSendJS : pCore
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsContentHtml { get; set; }

        public List<entAttachJS> Attachments { get; set; }
    }
}