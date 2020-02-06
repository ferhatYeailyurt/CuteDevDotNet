/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using CuteDev.Database;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Users;
using CuteDev.Entity.Results;
using CuteDev.Entity.Parameters;
using System.Data.Entity;
using CuteDev.Users.Data.Entity.UsersMeta;

namespace CuteDev.Users.Data.DAL
{

    /// <summary>
    /// Users üzerinde işlem yapar. (volkansendag - 24.02.2018)
    /// </summary>
    public class dalUsers : dalBase
    {

        /// <summary>
        /// Users olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public override bool Exist(Database.DAL.BaseModel entBase, DbContext db)
        {
            var userDB = (CuteModel)db;

            var ent = (Model.Users)entBase;

            if (ent.id > 0)
                return userDB.Users.Where(p => p.deleted == false && p.email == ent.email && p.id != ent.id).Any();

            return userDB.Users.Where(p => p.deleted == false && p.email == ent.email).Any();
        }

        /// <summary>
        /// Farklı Users olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public static bool DifferentExist(Model.Users ent, CuteModel db)
        {
            return db.Users.Where(p => p.deleted == false && p.email == ent.email && p.id != ent.id).Any();
        }

        /// <summary>
        /// Users getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rUsers GetById(decimal id, CuteModel db)
        {
            var query = (from p in db.Users
                         where p.deleted == false && p.id == id
                         select new rUsers
                         {
                             id = p.id,
                             email = p.email,
                             fullname = p.fullname,
                             role = p.role,
                         });

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Users getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rUsers GetByUserEmail(string email, CuteModel db)
        {
            var query = (from p in db.Users
                         where p.deleted == false && p.email == email
                         select new rUsers
                         {
                             id = p.id,
                             email = p.email,
                             fullname = p.fullname,
                             role = p.role,
                         });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Users getirir. (volkansendag - 24.02.2018)
        /// </summary>
        public static rUsers GetByUserEmailAndPassword(string email, string password, CuteModel db)
        {
            var query = (from p in db.Users
                         where p.deleted == false && p.email == email && p.password == password
                         select new rUsers
                         {
                             id = p.id,
                             email = p.email,
                             fullname = p.fullname,
                             role = p.role,
                         });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Users listeler. (volkansendag - 24.02.2018)
        /// </summary>
        public static int Count(Bilet blt, CuteModel db)
        {
            return db.Users.Count(p => !p.deleted);
        }

        /// <summary>
        /// Users listeler. (volkansendag - 24.02.2018)
        /// </summary>
        public static rUserProfile Info(Bilet blt, CuteModel db)
        {
            var query = (from p in db.Users
                         where p.deleted == false
                         && p.id == blt.KullaniciId
                         orderby p.id
                         select new rUserProfile
                         {
                             id = p.id,
                             email = p.email,
                             fullname = p.fullname,
                             role = p.role,
                         });

            return query.FirstOrDefault();
        }


        /// <summary>
        /// Users listeler. (volkansendag - 24.02.2018)
        /// </summary>
        public static rList<rUsers> List(pList prms, CuteModel db)
        {
            var query = (from p in db.Users
                         join meta in db.UsersMeta on p.id equals meta.user_Id into mtk
                         where p.deleted == false
                         orderby p.id
                         select new rUsers
                         {
                             id = p.id,
                             email = p.email,
                             fullname = p.fullname,
                             role = p.role,
                             phone = mtk.Where(a => a.metaKey == "phone").Select(x => x.metaValue).FirstOrDefault(),
                             company_logo = mtk.Where(a => a.metaKey == "company_logo").Select(x => x.metaValue).FirstOrDefault(),
                             company_name = mtk.Where(a => a.metaKey == "company_name").Select(x => x.metaValue).FirstOrDefault(),
                             company_adress = mtk.Where(a => a.metaKey == "company_adress").Select(x => x.metaValue).FirstOrDefault(),
                             company_sector = mtk.Where(a => a.metaKey == "company_sector").Select(x => x.metaValue).FirstOrDefault(),
                             company_status = mtk.Where(a => a.metaKey == "company_status").Select(x => x.metaValue).FirstOrDefault(),
                             company_employee = mtk.Where(a => a.metaKey == "company_employee").Select(x => x.metaValue).FirstOrDefault()
                         });

            return query.toList<rUsers>(prms);
        }

    }
}

