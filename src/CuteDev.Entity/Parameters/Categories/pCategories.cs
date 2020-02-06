/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Parameters.Categories
{

    /// <summary>
    /// Categories sonuç varlığı (volkansendag - 20.08.2015)
    /// </summary>
    public class pCategories : pId
    {
        public decimal? parentId { get; set; }

        public string name { get; set; }

        public string route { get; set; }

        public string color { get; set; }

    }
}
