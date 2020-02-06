using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CuteDev
{
    [Serializable]
    public class Bilet
    {
        public int KullaniciId { get; set; }

        public string IP { get; set; }

        public string SessionId { get; set; }

        public DateTime LoginTime { get; set; }

        public Bilet()
        {
            Initialize();
        }

        public Bilet(int kullaniciId, string ip = null)
            : this()
        {
            this.KullaniciId = kullaniciId;
            this.IP = ip;
        }

        private void Initialize()
        {
            this.IP = "127.0.0.1";
            this.LoginTime = DateTime.Now;
            this.SessionId = Guid.NewGuid().ToString();
        }



        public string BiletYarat()
        {
            string json = this.toJson();
            return new Crypto("GGGF29C2-B372-42EF-B07A-C72C86E27D80").TryEncrypt(json);
        }

        public static Bilet Oku(string bilet)
        {
            try
            {
                string json = new Crypto("GGGF29C2-B372-42EF-B07A-C72C86E27D80").TryDecrypt(bilet);

                var blt = json.toObjectFromJson<Bilet>();



                //if (blt !=null &&  blt.LoginTime.ToLocalTime() <= DateTime.Now.ToLocalTime().AddMinutes(-1))
                //    return null;

                return blt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
