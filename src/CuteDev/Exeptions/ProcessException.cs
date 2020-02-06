using CuteDev.Entity.Results;
using System;

namespace CuteDev
{
    [Serializable]
    public class ProcessException : Exception
    {
        #region Properties

        public string Code { get; set; }

        #endregion

        #region Constructor

        public ProcessException()
            : base()
        {

        }

        public ProcessException(string message)
            : base(message)
        {

        }

        public ProcessException(string code, string message)
            : base(message)
        {
            this.Code = code;
        }

        public ProcessException(string code, string message, params object[] prms)
            : base(message)
        {
            this.Code = code;
            this.Data.Add("ErrorData", prms);
        }

        #endregion

        #region Functions

        public rCore GetResult()
        {
            var result = new rCore();
            result.Message = this.Message;
            result.MessageCode = this.Code;
            result.Error = true;
            return result;
        }

        public T GetResult<T>() where T : rCore, new()
        {
            var result = new T();
            result.Message = this.Message;
            result.MessageCode = this.Code;
            result.Error = true;
            return result;
        }

        #endregion
    }
}
