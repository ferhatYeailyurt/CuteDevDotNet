/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Parameters.Files
{

    /// <summary>
    /// Files sonuç varlığı (volkansendag - 31.08.2015)
    /// </summary>
    public class pFiles : pId
    {
        public string file { get; set; }

        public string route { get; set; }

        public string format { get; set; }

        public byte type { get; set; }

        public string name { get; set; }
    }
}
