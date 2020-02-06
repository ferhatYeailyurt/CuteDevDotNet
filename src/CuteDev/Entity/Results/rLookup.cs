using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Entity.Results
{
    public class rLookup
    {
        public rLookup()
        {

        }

        public rLookup(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public int id { get; set; }
        public string text { get; set; }
    }
}
