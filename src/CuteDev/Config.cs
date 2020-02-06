using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace CuteDev
{
    public class Config
    {

        #region Properties
        public static string Guid
        {
            get { return ConfigurationManager.AppSettings["Guid"]; }
        }

        public static string Serial
        {
            get { return ConfigurationManager.AppSettings["Serial"]; }
        }

        public static string ConnectionString
        {
            get
            {
                try
                {
                    return new Crypto().Decrypt(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                }
                catch (Exception)
                {
                    if (ConfigurationManager.ConnectionStrings["constr"] != null)
                        return ConfigurationManager.ConnectionStrings["constr"].ToString();

                    throw new Exception("constr değeri çözülemedi!");
                }
            }
        }

        public static UserInfo Lisans
        {
            get
            {
                try
                {
                    string json = new Crypto(Serial).Decrypt(ConfigurationManager.AppSettings["License"]);
                    return new JavaScriptSerializer().Deserialize<UserInfo>(json);
                }
                catch (Exception)
                {
                    throw new Exception("Lisans geçerli olmadığından işleminiz iptal edildi!");
                }
            }
        }

        public static string MapPath
        {
            get
            {
                try
                {
                    string mapPath = System.Web.HttpContext.Current.Server.MapPath("~/").TrimEnd('\\');
                    return mapPath;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Web uygulamasının bulunduğu klasörün fiziksel adresi (volkansendag - 2015.03.31)
        /// </summary>
        public static string BaseHostUrl
        {
            get
            {
                try
                {
                    //Return variable declaration
                    var appPath = string.Empty;

                    //Getting the current context of HTTP request
                    var context = HttpContext.Current;

                    //Checking the current context content
                    if (context != null)
                    {
                        //Formatting the fully qualified website url/name
                        appPath = string.Format("{0}://{1}{2}{3}",
                                                context.Request.Url.Scheme,
                                                context.Request.Url.Host,
                                                context.Request.Url.Port == 80
                                                    ? string.Empty
                                                    : ":" + context.Request.Url.Port,
                                                context.Request.ApplicationPath);
                    }

                    if (!appPath.isEmpty() && !appPath.EndsWith("/"))
                        appPath += "/";

                    return appPath;
                }
                catch (Exception)
                {
                    //throw new Exception("BaseDirectory değeri okunamadı!");
                }
                return null;
            }
        }

        #endregion

    }
}
