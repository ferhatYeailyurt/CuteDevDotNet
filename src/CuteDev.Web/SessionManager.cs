using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace CuteDev.Web
{
    public class SessionManager
    {
        #region Globals

        public const string onlineUser = "CuteDev_OnlineUser";

        #endregion

        #region Properties

        public virtual bool UseCookie
        {
            get
            {
                var settings = ConfigurationManager.AppSettings["UseCookie"];

                if (settings != null && settings.ToLower() == "true")
                    return true;

                return false;
            }
        }

        public AuthenticatedUser OnlineUser
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[onlineUser] != null)
                    return HttpContext.Current.Session[onlineUser] as AuthenticatedUser;

                return null;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[onlineUser] = value;

                    if (value != null)
                        HttpContext.Current.Session.Timeout = 120;
                }
            }
        }

        #endregion

        #region Functions

        private string ClientIp
        {
            get
            {
                try
                {
                    if (System.Web.HttpContext.Current != null
                        && System.Web.HttpContext.Current.Request != null
                        && System.Web.HttpContext.Current.Request.UserHostName != null)
                        return System.Web.HttpContext.Current.Request.UserHostName;

                    return "127.0.0.1";
                }
                catch (Exception)
                {
                    return "127.0.0.1";
                }
            }
        }

        public string Login(int userId, object userInfo = null)
        {

            OnlineUser = new AuthenticatedUser(userId, userInfo);

            Bilet blt = new Bilet(userId, ClientIp);

            var token = blt.BiletYarat();

            if (UseCookie)
                saveTokenToCookie(token);

            return token;

        }

        public void KillSession()
        {
            OnlineUser = null;

            if (UseCookie)
                saveTokenToCookie("");

            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session.Abandon();
        }

        public bool RefreshSession()
        {
            AuthenticatedUser user = OnlineUser;
            return RefreshSession(user);
        }

        public bool RefreshSession(AuthenticatedUser user)
        {
            OnlineUser = user;

            if (user != null)
                return true;
            else
                return false;
        }


        private bool isExpired(Bilet blt)
        {
            int SessionTimeoutMinute = 30 /*gun*/ * 24 /*saat*/ * 60 /*dakika*/;

            if (blt == null)
                return true;

            var loginTime = blt.LoginTime.ToLocalTime();

            if (loginTime <= DateTime.Now.AddMinutes(-SessionTimeoutMinute))
            {
                return true;
            }

            return false;
        }

        public Bilet BiletAl(bool guvenliOturum = true, string bilet = null)
        {
            Bilet blt = null;

            if (blt == null && this.OnlineUser != null)
                blt = new Bilet(this.OnlineUser.Id, ClientIp);

            if (blt == null && !bilet.isEmpty())
            {
                blt = Bilet.Oku(bilet);
                if (isExpired(blt))
                    blt = null;
            }

            if (blt == null && UseCookie)
            {
                bilet = getTokenFromCookie();

                if (!bilet.isEmpty())
                {
                    blt = Bilet.Oku(bilet);

                    if (isExpired(blt))
                    {
                        refreshTokenOnCookie(blt);
                        //blt = null;
                    }
                }
            }

            if (blt == null && !guvenliOturum)
                blt = new Bilet(0, ClientIp);

            return blt;
        }

        private const string cookieName = ".CUTEAPP";

        public bool refreshTokenOnCookie(Bilet blt)
        {
            try
            {
                var token = blt.BiletYarat();

                saveTokenToCookie(token);

                return true;
            }
            catch (Exception)
            {
                //TODO.
                return false;
            }
        }

        public bool saveTokenToCookie(string token, HttpResponse response = null)
        {
            try
            {
                if (response == null && HttpContext.Current.Response != null)
                    response = HttpContext.Current.Response;

                if (response == null)
                    return false;

                HttpCookie objCookie = new HttpCookie(cookieName, token);
                objCookie.Expires = DateTime.Now.AddMonths(1);
                response.Cookies.Add(objCookie);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string getTokenFromCookie(HttpRequest request = null)
        {
            try
            {
                if (request == null && HttpContext.Current.Request != null)
                    request = HttpContext.Current.Request;

                if (request == null)
                    return null;

                HttpCookie objCookie = request.Cookies[cookieName];

                if (objCookie == null)
                    return null;

                return objCookie.Value;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion
    }
}
