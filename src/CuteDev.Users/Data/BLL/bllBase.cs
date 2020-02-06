/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Users.Data.DAL.Model;
using System;
using System.Threading.Tasks;

namespace CuteDev.Users.Data.BLL
{
    public class bllBase : CuteDev.Database.BLL.bllBase
    {
        public ProcessException getEx(Exception ex, Bilet blt)
        {
            LogYaz(ex.Message, blt, ex.ToString());

            return base.getEx(ex);
        }

        internal void LogYaz(Exception ex, Bilet blt = null)
        {
            LogYaz(ex.Message, blt, ex.ToString());
        }

        internal void LogYaz(string message, Bilet blt, params object[] prms)
        {
            Task.Run(() =>
            {
                try
                {
                    var log = new Log.Data.BLL.bllLogs();

                    log.Add(new Log.Entity.pLogs()
                    {
                        appName = "CuteDev.Youtube.Haber",
                        logLevel = "bll",
                        logData = prms.toJson(),
                        message = message
                    }, blt);
                }
                catch (Exception)
                {
                    // TODO 
                }
            });
        }

        internal static CuteModel getDb()
        {
            return new CuteModel();
        }
    }
}
