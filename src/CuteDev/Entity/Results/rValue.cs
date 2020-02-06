/* Author: Volkan ŞENDAĞ - volkansendag@belsis.com.tr - BELSİS ANKARA */
using System;
using System.Linq;

namespace CuteDev.Entity.Results
{
    /// <summary>
    /// Çekirdek servis sonucu (volkansendag - 2015.03.07)
    /// </summary>
    [Serializable]
    public class rValue<T> : rCore
    {
        #region Properties

        public T Value { get; set; }

        #endregion

        #region Constructor

        public rValue()
            : base()
        { }

        public rValue(T value)
            : this()
        {
            this.Value = value;
        }

        public rValue(bool error, string messageCode, string message)
            : base(error, messageCode, message)
        { }

        public rValue(IQueryable<T> query)
            : this(query.FirstOrDefault())
        { }

        #endregion
    }
}
