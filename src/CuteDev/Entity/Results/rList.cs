/* Author: Volkan ŞENDAĞ - volkansendag@belsis.com.tr - BELSİS ANKARA */
using System;
using System.Collections.Generic;
using System.Linq;

namespace CuteDev.Entity.Results
{
    /// <summary>
    /// Çekirdek paging servis parametresi (volkansendag - 2014.03.07)
    /// </summary>
    [Serializable]
    public class rList<T> : rCore
    {
        public int Count { get; set; }

        public List<T> Values { get; set; }

        public rList()
        {
            this.Values = new List<T>();
            this.Count = 0;
        }

        public rList(List<T> values, int count)
        {
            this.Values = values;
            this.Count = count;
        }

        public rList(List<T> values)
            : this(values.ToList(), values == null ? 0 : values.Count)
        { }


        public rList(IQueryable<T> query)
            : this(query.ToList())
        { }


        public rList(bool error, string messageCode, string message)
            : base(error, messageCode, message)
        { }

        public rList(IQueryable<T> query, Parameters.pList prms)
        {
            if (prms.take > 0)
            {
                this.Values = query.Skip(prms.skip).Take(prms.take).ToList();
                this.Count = query.Count();
            }
            else
            {
                this.Values = query.ToList();
                this.Count = this.Values.Count;
            }

        }

        public void Add(T val)
        {
            this.Values.Add(val);
            this.Count++;
        }
    }
}
