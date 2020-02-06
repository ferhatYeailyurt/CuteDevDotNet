/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Results.Files
{

    /// <summary>
    /// Files sonuç varlığı (volkansendag - 31.08.2015)
    /// </summary>

    public class rFiles
    {
        public decimal id { get; set; }

        public decimal creatorId { get; set; }

        public DateTime? updateDate { get; set; }

        public decimal? updaterId { get; set; }

        public string route { get; set; }

        public string localPath { get; set; }

        public string format { get; set; }

        public byte type { get; set; }

    }
}
