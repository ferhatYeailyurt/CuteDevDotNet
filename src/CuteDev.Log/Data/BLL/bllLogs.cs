/* Author: Volkan Sendag - vsendag@gmail.com */
using System;
using System.Linq;
using System.Collections.Generic;
using CuteDev.Database;
using CuteDev.Entity.Results;
using CuteDev.Entity.Parameters;
using CuteDev.Database.BLL;
using System.Configuration;
using CuteDev.Log.Entity;
using CuteDev.Log.Data.DAL.Model;

namespace CuteDev.Log.Data.BLL
{

    /// <summary>
    /// Logs üzerinde işlem yapar. (volkansendag - 02.08.2016)
    /// </summary>
    public class bllLogs : bllBase
    {
        private const string IslemSinifi = "bllPermissions";

        private DAL.dalLogs dal = new DAL.dalLogs();

        public void AddDefaultPermissions(CuteModel db, Bilet blt = null)
        {

        }

        /// <summary>
        /// Logs ekler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<decimal> Add(pLogs prms, Bilet blt, CuteModel db)
        {
            if (prms.appName.isEmpty())
                throw Exceptions.Parameter("Uygulama bulunamadı");
            if (prms.message.isEmpty())
                throw Exceptions.Parameter("Mesaj boş olamaz.");

            var ent = new DAL.Model.Logs();

            if (blt == null)
            {
                blt = new Bilet();
            }


            ent.appName = prms.appName;
            ent.logLevel = prms.logLevel;
            ent.message = prms.message;
            ent.guid = Guid.NewGuid().ToString();
            ent.createDate = DateTime.Now;
            ent.creatorIP = blt.IP;
            ent.creatorId = blt.KullaniciId;
            ent.logData = prms.logData;


            Database.DAL.dalBase.Add(ent, db);

            return new rValue<decimal>(ent.id);
        }

        internal rValue<decimal> Add(string message, Bilet blt, CuteModel db)
        {
            return Add(new pLogs(message), blt, db);
        }

        /// <summary>
        /// Logs ekler. (volkansendag - 02.08.2016)
        /// </summary>
        public rValue<decimal> Add(pLogs prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return Add(prms, blt, db);
                }
                catch (ProcessException ex)
                {
                    return ex.GetResult<rValue<decimal>>();
                }
            }
        }


        /// <summary>
        /// Logs listeler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rList<rLogs> List(pList prms, Bilet blt, CuteModel db)
        {
            Add(new pLogs()
            {
                appName = "CuteDev.Log",
                logData = prms.toJson(),
                logLevel = "bllLog",
                message = "Loglar listelendi"
            }, blt, db);

            return dal.List(prms, blt, db);
        }

        /// <summary>
        /// Logs listeler. (volkansendag - 02.08.2016)
        /// </summary>
        public rList<rLogs> List(pList prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return List(prms, blt, db);
                }
                catch (ProcessException ex)
                {
                    return ex.GetResult<rList<rLogs>>();
                }
            }
        }
    }
}
