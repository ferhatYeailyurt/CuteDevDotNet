using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace CuteDev.Api
{
    public class ApiRoute : IRouteHandler
    {
        #region Globals

        string template = "";

        #endregion

        #region Cosntructor

        public ApiRoute(string _template = "~/data/{api}.ashx")
        {
            this.template = _template;
        }

        #endregion

        #region Functions

        public IHttpHandler GetHttpHandler(RequestContext context)
        {
            string vPath = this.template.Replace("{api}", context.RouteData.Values["api"].ToString());
            try
            {
                IHttpHandler httpHandler = (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(vPath, typeof(IHttpHandler));
                return httpHandler;
            }
            catch (Exception ex)
            {
                return new ApiErrorBase(ex);
            }

        }

        #endregion
    }
}
