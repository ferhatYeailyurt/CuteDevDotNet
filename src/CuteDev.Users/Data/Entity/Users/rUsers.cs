using CuteDev.Users.Data.Entity.UsersMeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CuteDev.Users.Data.Entity.Users
{
    public class rUsers
    {
        public int id { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string phone { get; set; }
        public string company_name { get; set; }
        public string company_sector { get; set; }
        public string company_status { get; set; }
        public string company_employee { get; set; } 
        public string company_adress { get; set; }
        public string company_logo { get; internal set; }
    }
}
