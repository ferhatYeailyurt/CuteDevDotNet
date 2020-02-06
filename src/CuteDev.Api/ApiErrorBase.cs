using CuteDev.Web;
using System.Web;

namespace CuteDev.Api
{
    public class ApiErrorBase : apiBase
    {
        #region Functions

        private string message;

        public ApiErrorBase(System.Exception ex)
        {
            this.message = ex.Message;
        }

        public ApiErrorBase(string message)
        {
            this.message = message;
        }

        public override void ProcessRequest(HttpContext context)
        {
            this.Context = context;

            if (message == null)
            {
                base.SendApiNotFoundError();
                return;
            }
            else
            {
                base.SendError(Exceptions.Codes.ApiNotFound, message);
                return;
            }
        }

        #endregion
    }
}
