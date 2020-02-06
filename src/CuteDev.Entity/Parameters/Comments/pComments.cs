/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Parameters.Comments
{

    /// <summary>
    /// Comments sonuç varlığı (volkansendag - 20.08.2015)
    /// </summary>
    public class pComments : pId
    {
        public decimal parentId { get; set; }

        public byte type { get; set; }

        public string comment { get; set; }

        public bool confirmed { get; set; }

    }
}
