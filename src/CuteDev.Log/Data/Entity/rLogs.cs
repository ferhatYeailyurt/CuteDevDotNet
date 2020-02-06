/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Log.Entity
{

    /// <summary>
    /// Logs sonuç varlığı (volkansendag - 02.08.2016)
    /// </summary>

    public class rLogs
    {
        public decimal id { get; set; }
        public string appName { get; set; }
        public DateTime createDate { get; set; }
        public decimal creatorId { get; set; }
        public string creatorIP { get; set; }
        public string logLevel { get; set; }
        public string message { get; set; }
        public string logData { get; set; }
    }
}
