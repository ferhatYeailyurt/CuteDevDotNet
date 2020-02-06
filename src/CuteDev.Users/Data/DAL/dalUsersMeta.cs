/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using CuteDev.Database;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.UsersMeta;
using CuteDev.Entity.Results;
using System.Data.Entity;

namespace CuteDev.Users.Data.DAL
{

    /// <summary>
    /// UsersMeta üzerinde işlem yapar. (volkansendag - 24.02.2018)
    /// </summary>
    public class dalUsersMeta : dalBase
    {

        /// <summary>
        /// UsersMeta olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public override bool Exist(Database.DAL.BaseModel entBase, DbContext db)
        {
            var dbUser = (CuteModel)db;
            var ent = (UsersMeta)entBase;
            if (ent.id > 0)
                return dbUser.UsersMeta.Where(p => p.deleted == false && p.user_Id == ent.user_Id && p.metaKey == ent.metaKey && p.id != ent.id).Any();

            return dbUser.UsersMeta.Where(p => p.deleted == false && p.user_Id == ent.user_Id && p.metaKey == ent.metaKey).Any();
        }

        /// <summary>
        /// UsersMeta getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rUsersMeta GetById(decimal id, CuteModel db)
        {
            var query = (from um in db.UsersMeta
                         where um.deleted == false
                         select new rUsersMeta
                         {
                             id = um.id,
                             user_Id = um.user_Id,
                             metaKey = um.metaKey,
                             metaValue = um.metaValue,
                         });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// UsersMeta getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static string GetValueByKey(string key, Bilet blt, CuteModel db)
        {
            var query = (from um in db.UsersMeta
                         where um.deleted == false
                            && um.user_Id == blt.KullaniciId
                            && um.metaKey == key
                         select um.metaValue);

            return query.FirstOrDefault();
        }

        /// <summary>
        /// UsersMeta getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rValue<rUsersMeta> GetByKeyAndUserId(string key, decimal userId, CuteModel db)
        {
            var query = (from um in db.UsersMeta
                         where um.deleted == false
                            && um.user_Id == userId
                            && um.metaKey == key
                         select new rUsersMeta
                         {
                             id = um.id,
                             user_Id = um.user_Id,
                             metaKey = um.metaKey,
                             metaValue = um.metaValue,
                         });

            return query.toSingle<rUsersMeta>();
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 24.02.2018)
        /// </summary>
        public static rList<rUsersMeta> ListByUserId(decimal userId, CuteModel db)
        {
            var query = (from um in db.UsersMeta
                         where um.deleted == false
                            && um.user_Id == userId
                         orderby um.id
                         select new rUsersMeta
                         {
                             id = um.id,
                             user_Id = um.user_Id,
                             metaKey = um.metaKey,
                             metaValue = um.metaValue,
                         });

            return query.toList<rUsersMeta>();
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 24.02.2018)
        /// </summary>
        public static rList<rUsersMeta> ListByCurrentUserId(Bilet blt, CuteModel db)
        {
            var query = (from um in db.UsersMeta
                         where um.deleted == false
                            && um.user_Id == blt.KullaniciId
                         orderby um.id
                         select new rUsersMeta
                         {
                             id = um.id,
                             deleted = um.deleted,
                             user_Id = um.user_Id,
                             metaKey = um.metaKey,
                             metaValue = um.metaValue,
                         });

            return query.toList<rUsersMeta>();
        }
    }
}

