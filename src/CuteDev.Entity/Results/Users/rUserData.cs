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

    public class rUserData
    {
        public string fullname { get; set; }

        public DateTime? birthdate { get; set; }

        public byte gender { get; set; }

        public string mail { get; set; }

        public string address { get; set; }

        public string phone { get; set; }
        
        public string password { get; set; }

        public string photo { get; set; }

        public string fbUserId { get; set; }

        public string photoProfile { get; set; }

    }
}
