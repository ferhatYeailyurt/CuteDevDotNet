/* Author: Volkan Şendağ - volkansendag@belsis.com.tr */
using CuteDev.Database;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System;

namespace CuteDev.Database.DAL
{
    /// <summary>
    /// DAL içerisinde yapılan ortak işlemleri yapar. (volkansendag - 2015.04.14)
    /// </summary>
    public abstract class dalBase
    {


        /// <summary>
        /// Veritabanına kayıt ekler. (volkansendag - 2015.04.14)
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="db"></param>
        public static void Add(object ent, CuteModel db)
        {
            db.Set(ent.GetType()).Add(ent);
            db.SaveChanges();
        }

        /// <summary>
        /// Veritabanına kayıt ekler. (volkansendag - 2015.04.14)
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="db"></param>
        public static void AddBatch<T>(List<T> ents, DbContext db, int batchSize = 100) where T : class
        {
            if (ents.Count <= 0)
                return;

            db.Configuration.AutoDetectChangesEnabled = false;

            for (int i = 0; i <= ents.Count / batchSize; i++)
            {
                db.Set(ents[0].GetType()).AddRange(ents.Skip(i * batchSize).Take(batchSize).ToList());

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Veritabanındaki kayıdı günceller. (volkansendag - 2015.04.14)
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="db"></param>
        public static void Update(object ent, DbContext db)
        {
            db.SaveChanges();
        }

        /// <summary>
        /// Veritabanından kayıt siler. (volkansendag - 2015.04.14)
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="db"></param>
        public static void Delete(object ent, DbContext db)
        {
            db.SaveChanges();
        }

        public static T Get<T>(decimal? id = null, DbContext db = null) where T : BaseModel, new()
        {
            if (id == null)
                return new T();

            return db.Set<T>().SingleOrDefault(p => p.id == id);
        }

        public virtual bool Exist(BaseModel ent, DbContext db)
        {
            return false;
            //return db.Users.Where(p => p.deleted == false && p.email == ent.email).Any();
        }

        public virtual bool Exist(BaseModel ent, Bilet blt, DbContext db)
        {
            return false;
            //return db.Users.Where(p => p.deleted == false && p.email == ent.email).Any();
        }
    }
}
