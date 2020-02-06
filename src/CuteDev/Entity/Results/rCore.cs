/* Author: Volkan Şendağ - volkansendag@belsis.com.tr */
using System;

namespace CuteDev.Entity.Results
{

    /// <summary>
    /// Core sonuç varlığı (volkansendag - 09.10.2014)
    /// </summary>
    [Serializable]
    public class rCore
    {
        #region Properties

        public bool Error { get; set; }

        public string MessageCode { get; set; }

        public string Message { get; set; }

        #endregion

        #region Constructor

        public rCore()
        { }

        public rCore(bool error, string messageCode, string message)
        {
            this.Error = error;
            this.MessageCode = messageCode;
            this.Message = message;
        }

        #endregion
    }
}
