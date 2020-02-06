/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using CuteDev.Entity.Results;

namespace CuteDev.Entity.Results.UserType
{

    /// <summary>
    /// UserType Listele sonucu. (volkansendag - 20.08.2015)
    /// </summary>
    [Serializable]
    public class rListeleUserType : rCore
    {
        #region Properties

        public List<rUserType> Values { get; set; }

        public decimal Count { get; set; }

        #endregion

        #region Constructor

        public rListeleUserType()
        {
            this.Values = new List<rUserType>();
            this.Count = 0;
        }

        public rListeleUserType(List<rUserType> val)
        {
            this.Values = val;
            this.Count = val.Count;
        }

        public rListeleUserType(List<rUserType> val, decimal count)
        {
            this.Values = val;
            this.Count = count;
        }

        #endregion
    }
}
