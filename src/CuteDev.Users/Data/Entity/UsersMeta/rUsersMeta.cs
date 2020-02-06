using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Users.Data.Entity.UsersMeta
{
    public class rUsersMeta
    {
        public int id { get; set; }
        public int user_Id { get; set; }
        public bool deleted { get; set; }
        public string metaKey { get; set; }
        public string metaValue { get; set; }
    }
}
