/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Results.UserType
{

    /// <summary>
    /// UserType sonuç varlığı (volkansendag - 20.08.2015)
    /// </summary>

    public class rUserType
    {
        public decimal id { get; set; }

        public decimal creatorId { get; set; }

        public DateTime? updateDate { get; set; }

        public decimal? updaterId { get; set; }

        public string name { get; set; }

    }
}
