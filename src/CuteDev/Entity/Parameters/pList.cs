using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Entity.Parameters
{
    [Serializable]
    public class pList : pId
    {
        public int take { get; set; }

        public int skip { get; set; }

        public filter filter { get; set; }

        public List<sort> sort { get; set; }

        public pList() : base() { }
        public pList(int id) : base(id) { }

    }

    [Serializable]
    public class filter
    {
        public List<filterItem> filters { get; set; }
        public string logic { get; set; }
    }

    [Serializable]
    public class filterItem
    {
        public List<filterItem> filters { get; set; }

        public string field { get; set; }

        public string @operator { get; set; }

        public string value { get; set; }

    }

    [Serializable]
    public class sort
    {
        public string dir { get; set; }

        public string field { get; set; }
    }
}
