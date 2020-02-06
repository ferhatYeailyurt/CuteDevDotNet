/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Parameters.Users
{

    /// <summary>
    /// pUserLogin parametre varlığı (volkansendag - 14.09.2015)
    /// </summary>
    public class pUserLogin : pId
    {
        public string username { get; set; }

        public string password { get; set; }

        public string token { get; set; }

        public string userID { get; set; }

    }
}
