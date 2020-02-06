using CuteDev.Web;
using System.Web;

namespace CuteDev.Api
{
    public abstract class apiAuthenticatedBase : apiBase
    {
        #region Functions

        public override void ProcessRequest(HttpContext context)
        {
            this.Context = context;

            //bllSession.KeepOnline();
            var sm = new SessionManager();

            if (sm.OnlineUser == null)
            {
                base.SendSessionError();
                return;
            }

            base.ProcessRequest(context);
        }

        #endregion
    }
}
