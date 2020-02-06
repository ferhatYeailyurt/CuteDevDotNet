/* Author: Volkan Şendağ- volkansendag@belsis.com.tr - BELSİS ANKARA */
using System;
using System.Collections.Generic;

namespace CuteDev.Entity.Parameters
{
    /// <summary>
    /// Çekirdek pId parametresi (volkansendag - 2015.05.12)
    /// </summary>
    [Serializable]
    public class pIds : pCore
    {
        public int? id { get; set; }
        public List<int> ids { get; set; }

    }
}
