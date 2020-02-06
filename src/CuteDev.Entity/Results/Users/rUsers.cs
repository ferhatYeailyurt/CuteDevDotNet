/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CuteDev.Entity.Results.Users
{

    /// <summary>
    /// Users sonuç varlığı (volkansendag - 20.08.2015)
    /// </summary>

    public class rUsers
    {
        public decimal id { get; set; }

        public string username { get; set; }

        public bool active { get; set; }

        public byte type { get; set; }

        public string mail { get; set; }

        public string password { get; set; }

        [ScriptIgnore]
        public string dataStr { get; set; }

        public rUserData data { get; set; }
    }
}
