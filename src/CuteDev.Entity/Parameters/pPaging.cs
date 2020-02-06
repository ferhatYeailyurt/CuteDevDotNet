/* Author: Volkan Şendağ - volkansendag@belsis.com.tr */
using System;

namespace CuteDev.Entity.Parameters
{

	/// <summary>
    /// Paging parametre varlığı (volkansendag - 09.10.2014)
    /// </summary>
	public class pPaging: pId
    {
        public pFilter Filter { get; set; }

        private int _take;
        public int Take
        {
            get { return _take <= 0 ? 10 : _take; }
            set { _take = value; }
        }

        public int Skip { get; set; }

        public OrderBy Order { get; set; }

        #region Constructor

        public pPaging()
        {
            Initialize();
        }

        #endregion

        #region Functions

        private void Initialize()
        {
            this.Order = new OrderBy();
            this.Order.OrderByAsc = true;
        }

        #endregion

    }

    [Serializable]
    public class OrderBy
    {
        public string FieldName { get; set; }

        public bool OrderByAsc { get; set; }
    }
}
