/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuteDev.Entity.Parameters;

namespace CuteDev.Users.Data.Entity.Users
{

    /// <summary>
    /// Users sonuç varlığı (volkansendag - 02.08.2016)
    /// </summary>
    public class pUsersSignup : pCore
    {
        public string email { get; set; }

        public string password { get; set; }

        public string fullname { get; set; }
    }
}
