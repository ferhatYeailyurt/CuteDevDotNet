using CuteDev.Web.Google.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CuteDev.Web.Google
{
    public class SearchManager
    {
        private int resultSize { get; set; }

        private SearchType seacrhType { get; set; }

        private string userip
        {
            get
            {
                try
                {
                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    if (ip.Length < 7)
                        return null;

                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public SearchManager(int rs, SearchType st)
        {
            this.resultSize = rs;
            this.seacrhType = st;
        }


        public rGoogleSearch Search(string query, int start)
        {

            if (start + this.resultSize > 64)
                this.resultSize = 64 - start;

            var url = String.Format("https://ajax.googleapis.com/ajax/services/search/{0}", this.seacrhType.ToString());
            url = url + String.Format("?v=1.0&hl=tr&rsz={0}&q={1}&start={2}", this.resultSize, query, start);

            if (!string.IsNullOrEmpty(userip))
                url = url + String.Format("&userip={0}", userip);

            try
            {
                string result = String.Empty;
                using (var client = new WebClient())
                {
                    result = client.DownloadString(url);
                }

                return result.toObjectFromJson<rGoogleSearch>();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
