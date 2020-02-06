/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuteDev.Entity.Parameters;

namespace CuteDev.Log.Entity
{

    /// <summary>
    /// Logs sonuç varlığı (volkansendag - 02.08.2016)
    /// </summary>
    public class pLogs : pCore
    {
        public decimal? id { get; set; }
        public string appName { get; set; }
        public string logLevel { get; set; }
        public string message { get; set; }
        public string logData { get; set; }

        public pLogs()
        {
            this.appName = "CuteDev.Log.Entity";
            this.logLevel = "0";
        }

        public pLogs(string message)
        {
            this.message = message;
            this.appName = "CuteDev.Log.Entity";
            this.logLevel = "0";
        }
    }
}
