using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Users.Data.Entity.UsersMeta
{
    public class pUsersMeta
    {
        public List<pUsersMeta> List { get; set; }

        public string metaKey { get; set; }
        public string metaValue { get; set; }
    }
}
