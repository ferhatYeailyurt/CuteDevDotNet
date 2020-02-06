using CuteDev.Entity.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Users.Data.Entity.Users
{
    public class pUsers : pCore
    {
        public int? id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
    }
}
