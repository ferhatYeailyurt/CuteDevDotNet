using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace CuteDev.Web
{
    public class AuthenticatedPageBase : Page
    {
        public SessionManager sm = new SessionManager();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string pageName = this.GetType().Name;

            var bilet = sm.BiletAl(true);

            if (bilet == null)
            {
                Redirect();
            }
            else
            {
                sm.RefreshSession();
            }
        }

        public virtual void Redirect()
        {
            Response.Redirect("Login.aspx");
        }

        public virtual void Redirect(string url)
        {
            Response.Redirect(url);
        }
    }
}
