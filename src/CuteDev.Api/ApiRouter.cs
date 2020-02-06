using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace CuteDev.Api
{
    public static class ApiRouter
    {
        #region Functions

        public static void Init()
        {
            RouteTable.Routes.Add("data", new Route("data/{api}/{method}", new ApiRoute("~/Api/Handlers/{api}.ashx")));
        }

        #endregion
    }
}
