/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Users.Data.Entity.Users
{

    /// <summary>
    /// Users sonuç varlığı (volkansendag - 20.08.2015)
    /// </summary>
    public class pUserChangePassword : pId
    {
        public string oldPassword { get; set; }

        public string newPassword { get; set; }

        public string newPasswordCheck { get; set; }

    }
}
