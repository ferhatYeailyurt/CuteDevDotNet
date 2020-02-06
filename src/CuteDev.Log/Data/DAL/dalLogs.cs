/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using System.Collections.Generic;
using CuteDev.Database;
using CuteDev.Entity.Results;
using CuteDev.Entity.Parameters;
using CuteDev.Log.Entity;
using CuteDev.Log.Data.DAL.Model;

namespace CuteDev.Log.Data.DAL
{

    /// <summary>
    /// Logs üzerinde işlem yapar. (volkansendag - 08.10.2017)
    /// </summary>
    public class dalLogs : CuteDev.Database.DAL.dalBase
    {
        /// <summary>
        /// Logs listeler. (volkansendag - 08.10.2017)
        /// </summary>
        public rList<rLogs> List(pList prms, Bilet blt, CuteModel db)
        {
            var query = (from p in db.Logs
                         orderby p.id descending
                         select new rLogs
                         {
                             id = p.id,
                             createDate = p.createDate,
                             creatorId = p.creatorId,
                             creatorIP = p.creatorIP,
                             appName = p.appName,
                             logLevel = p.logLevel,
                             message = p.message,
                             logData = p.logData,
                         });

            return query.toList<rLogs>(prms);
        }

    }
}

