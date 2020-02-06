/* Author: Volkan Şendağ - volkansendag@belsis.com.tr */
using System;
using System.Collections.Generic;

namespace CuteDev.Entity.Parameters
{

	/// <summary>
    /// Filter parametre varlığı (volkansendag - 09.10.2014)
    /// </summary>
	public class pFilter
    {
        public List<FilterItem> Filters { get; set; }

        public string Logic { get; set; }

        #region Constructor

        public pFilter()
        {
            this.Filters = new List<FilterItem>();
            this.Logic = "and";
        }

        public pFilter(List<FilterItem> list)
        {
            this.Filters = list;
            this.Logic = "and";

        }

        public pFilter(List<FilterItem> list, string logic)
        {
            this.Filters = list;
            this.Logic = logic;

        }

        #endregion

    }

    public class FilterItem{

        public string PropertyName { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }
    }
}
