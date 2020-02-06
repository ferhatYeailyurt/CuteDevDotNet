using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using CuteDev.Web;
using CuteDev.Entity.Results;
using CuteDev.Entity.Parameters;

namespace CuteDev.Api
{
    public abstract class apiBase : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region Properties

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private bool Redirect = false;
        private string RedirectUrl = "";

        protected HttpContext Context { get; set; }

        public string Content { get; private set; }

        public void redirect(string url)
        {
            this.Redirect = true;
            this.RedirectUrl = url;
        }

        public T getParams<T>() where T : class, new()
        {
            try
            {
                string contentType = Context.Request.ContentType;
                string ct = contentType.Split(';').First().Trim();

                if (ct == "application/json")
                {
                    return this.Content.toObjectFromJson<T>();
                }
                else if (ct == "multipart/form-data")
                {
                    this.Files = this.Context.Request.Files;
                    return new T();
                }
                else
                {
                    return this.Content.toObjectFromFormData<T>();
                }
            }
            catch (Exception)
            {
                return new T();
            }
        }

        public virtual bool ValidateJson
        {
            get
            {
                return false;
            }
        }

        public HttpFileCollection Files { get; private set; }

        public ApiMethod MetodInfo { get; private set; }
        public MethodInfo Method { get; private set; }


        public Bilet bilet;

        #endregion

        #region Functions

        public virtual void ProcessRequest(HttpContext context)
        {
            this.Context = context;
            this.Content = this.GetContent();

            if (this.ValidateJson && this.Content.isEmpty())
            {
                this.SendContentError();
                return;
            }

            MethodInfo method = this.GetMethod();


            if (method != null)
            {
                var attrs = method.GetCustomAttributes(typeof(ApiMethod), true);

                this.MetodInfo = (ApiMethod)attrs.FirstOrDefault();

                var prms = getParams<pCore>();

                if (prms != null)
                    bilet = this.BiletAl(this.MetodInfo.sessionSecure, prms.Bilet);

                if (this.MetodInfo.sessionSecure && bilet == null)
                {
                    this.SendSessionError();
                    return;
                }

                if (!this.MetodInfo.permissionId.isEmpty())
                {
                    var hasPerm = CheckPermission(bilet);

                    if (!hasPerm)
                    {
                        this.SendAuthorizationError();
                        return;
                    }
                }

                try
                {
                    var result = method.Invoke(this, null);
                    if (this.Redirect && !this.RedirectUrl.isEmpty())
                    {
                        Context.Response.Redirect(RedirectUrl, false);
                        return;
                    }
                    else
                    {
                        this.SendJson(result);
                    }
                }
                catch (Exception ex)
                {
                    string message = getExMessage(ex);

                    this.SendError("Invoke", message, 500);
                }
            }
        }

        Exception getException(Exception ex)
        {
            if (ex.InnerException != null)
                return getException(ex.InnerException);

            return ex;
        }


        public string getExMessage(Exception ex)
        {
            var ex2 = getException(ex);

            return ex2.Message;
        }

        public virtual bool CheckPermission(Bilet blt)
        {
            return false;
        }

        protected MethodInfo GetMethod()
        {
            if (this.Context.Request.RequestContext.RouteData.Values["method"] == null)
            {
                this.SendMethodError();
                return null;
            }

            MethodInfo method = this.GetType().GetMethod(this.Context.Request.RequestContext.RouteData.Values["method"].ToString());

            if (method == null)
            {
                this.SendMethodError();
                return null;
            }
            else
            {
                var attrs = method.GetCustomAttributes(typeof(ApiMethod), true);

                if (attrs.Length == 0)
                {
                    this.SendMethodError();
                    return null;
                }



                return method;
            }
        }

        protected void SendJson(object obj)
        {
            if ((bool)obj.GetPropValue("Error") == true)
                this.Context.Response.StatusCode = 505;


            this.Context.Response.ContentType = "application/json";

            var json = obj.toJson();

            var r = getParams<pCoreJson>();

            if (r != null && !r.callback.isEmpty())
            {
                json = string.Format("{0}({1})", r.callback, json);
            }

            this.Context.Response.Write(json);
        }

        protected string GetContent()
        {
            string result = null;

            if (this.Context.Request.HttpMethod == "GET")
            {
                result = this.Context.Request.QueryString.ToString();
            }
            else if (this.Context.Request.HttpMethod == "POST")
            {
                this.Context.Request.InputStream.Position = 0;

                using (var inputStream = new StreamReader(this.Context.Request.InputStream))
                {
                    result = inputStream.ReadToEnd();
                }
            }

            return result;
        }

        protected void SendError(string code, string text, int statusCode = 500)
        {
            rCore result = new rCore();
            result.Error = true;
            result.MessageCode = code;
            result.Message = text;

            //Log.LogManager.Add(new Log.Entity.pLogs()
            //{
            //    appName = "CuteDev.Api",
            //    logLevel = code,
            //    message = text
            //});

            this.SendJson(result);
            this.Context.Response.StatusCode = statusCode;
        }

        protected void SendAuthenticateError(string message = "İşlem yapmak için oturumunuz bulunamadı.")
        {
            SendError("Authorization", message, 401);
            this.Context.Response.AddHeader("WWW-Authenticate", "Basic");
        }

        protected void SendAuthorizationError(string message = "İşlem yapmaya yetkiniz bulunamadı..")
        {
            SendError("Authorization", message, 403);
        }



        protected void SendContentError(string message = "Eksik ya da hatalı parametre girdiniz!")
        {
            this.SendError(Exceptions.Codes.Parameter, message, 505);
        }

        protected void SendMethodError()
        {
            this.SendError(Exceptions.Codes.Method, Exceptions.Messages.Method, 505);
        }

        protected void SendSessionError()
        {
            this.SendError(Exceptions.Codes.Session, Exceptions.Messages.Session, 403);
        }

        protected void SendApiNotFoundError()
        {
            this.SendError(Exceptions.Codes.ApiNotFound, Exceptions.Messages.ApiNotFound, 404);
        }

        public Bilet BiletAl(bool guvenliOturum = true, string bilet = null)
        {
            if (bilet.isEmpty())
            {
                var prm = getParams<pCore>();
                bilet = prm.Bilet;
            }

            var sm = new SessionManager();
            return sm.BiletAl(guvenliOturum, bilet);
        }

        #endregion
    }

    public class pCoreJson : pCore
    {
        public string callback { get; set; }
    }
}
