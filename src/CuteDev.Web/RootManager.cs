using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace CuteDev.Web
{
    public class RootManager
    {

        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("", "Haberler/{pageNumber}", "~/Pages/Haberler.aspx");
            routeCollection.MapPageRoute("", "Haberler", "~/Pages/Haberler.aspx");
            routeCollection.MapPageRoute("", "Haber/{konuUrl}.html", "~/Pages/Haber.aspx");
        }
    }
}
