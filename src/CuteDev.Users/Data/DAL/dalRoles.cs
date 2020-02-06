/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Roles;
using System.Data.Entity;

namespace CuteDev.Users.Data.DAL
{

    /// <summary>
    /// Roles üzerinde işlem yapar. (volkansendag - 24.02.2018)
    /// </summary>
    public class dalRoles : dalBase
    {

        /// <summary>
        /// Roles olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public override bool Exist(Database.DAL.BaseModel entBase, DbContext db)
        {
            var permDB = (CuteModel)db;
            var ent = (Roles)entBase;
            if (ent.id > 0)
                return permDB.Roles.Where(p => p.deleted == false && p.title == ent.title && p.id != ent.id).Any();

            return permDB.Roles.Where(p => p.deleted == false && p.title == ent.title).Any();
        }

        /// <summary>
        /// Roles getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rRoles GetById(decimal id, CuteModel db)
        {
            var query = (from um in db.Roles
                         where um.deleted == false
                            && um.id == id
                         select new rRoles
                         {
                             id = um.id,
                             title = um.title,
                         });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Roles getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rRoles GetByTitle(string title, CuteModel db)
        {
            var query = (from um in db.Roles
                         where um.deleted == false
                            && um.title == title
                         select new rRoles
                         {
                             id = um.id,
                             title = um.title,
                         });

            return query.FirstOrDefault();
        }

    }
}

