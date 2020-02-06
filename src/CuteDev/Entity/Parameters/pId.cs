/* Author: Volkan Şendağ- volkansendag@belsis.com.tr - BELSİS ANKARA */
using System;

namespace CuteDev.Entity.Parameters
{
    /// <summary>
    /// Çekirdek pId parametresi (volkansendag - 2015.05.12)
    /// </summary>
    [Serializable]
    public class pId : pCore
    {
        public int? id { get; set; }

        public pId() { }

        public pId(int _id) { this.id = _id; }
    }
}
