/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Users;
using CuteDev.Users.Data.Entity.UsersMeta;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace CuteDev.Users.Data.BLL
{
    public class bllUsersMeta : bllBase
    {
        /// <summary>
        /// UsersMeta günceller. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<rCore> Update(pUsersMeta prms, Bilet blt, CuteModel db)
        {
            if (prms.List == null && !prms.metaKey.isEmpty())
            {
                prms.List = new List<pUsersMeta>() { new pUsersMeta() { metaKey = prms.metaKey, metaValue = prms.metaValue } };
            }

            if (prms.List == null || prms.List.Any(p => string.IsNullOrEmpty(p.metaKey)))
                throw Exceptions.Parameter();

            var names = prms.List.Select(p => p.metaKey).ToList();

            var ents = db.UsersMeta.Where(p => names.Contains(p.metaKey) && p.user_Id == blt.KullaniciId);

            foreach (var ent in ents)
            {
                var prm = prms.List.FirstOrDefault(p => p.metaKey == ent.metaKey);

                ent.metaValue = prm.metaValue;
                Update(ent, blt, db);
                names.RemoveAll(p => p == prm.metaKey);
            }

            if (names != null && names.Count > 0)
            {
                foreach (var metaKey in names)
                {
                    var prm = prms.List.FirstOrDefault(p => p.metaKey == metaKey);

                    var ent = new UsersMeta()
                    {
                        metaKey = metaKey,
                        user_Id = blt.KullaniciId,
                        metaValue = prm.metaValue,
                    };

                    Add(ent, blt, db);

                }
            }

            //LogYaz("Kullanıcı ayarları güncellendi.", blt);

            return new rValue<rCore>();
        }

        /// <summary>
        /// UsersMeta günceller. (volkansendag - 02.08.2016)
        /// </summary>
        public rValue<rCore> Update(pUsersMeta prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = Update(prms, blt, db);
                    trns.Commit();
                    return result;
                }
                catch (Exception)
                {
                    trns.Rollback();
                    //bllLog.Ekle(IslemSinifi, IslemMetodlari.Ekle, ex, ticket);
                    throw;
                }
            }
        }


        /// <summary>
        /// UsersMeta getirir. (volkansendag - 02.08.2016)
        /// </summary>
        internal rUsersMeta GetById(decimal id, Bilet blt, CuteModel db)
        {
            if (id <= 0)
                throw Exceptions.Parameter();

            //LogYaz("Kullanıcı ayarları getirildi.", blt);

            return dalUsersMeta.GetById(id, db);
        }

        /// <summary>
        /// UsersMeta getirir. (volkansendag - 02.08.2016)
        /// </summary>
        internal string GetValueByKey(string key, Bilet blt, CuteModel db)
        {
            if (key.isEmpty() || blt == null)
                throw Exceptions.Parameter();

            //LogYaz(metaKey + " isimli kullanıcı ayarı getirildi.", blt);

            return dalUsersMeta.GetValueByKey(key, blt, db);
        }

        /// <summary>
        /// UsersMeta getirir. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<rUsersMeta> GetByKeyAndUserId(string key, int userId, Bilet blt, CuteModel db)
        {
            if (key.isEmpty() || blt == null)
                throw Exceptions.Parameter();

            //LogYaz(metaKey + " isimli kullanıcı ayarı getirildi.", blt);

            return dalUsersMeta.GetByKeyAndUserId(key, userId, db);
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 02.08.2016)
        /// </summary>
        public rValue<rUsersMeta> GetByKeyAndUserId(string key, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return GetByKeyAndUserId(key, blt.KullaniciId, blt, db);
                }
                catch (Exception ex)
                {
                    //bllLog.Ekle(IslemSinifi, IslemMetodlari.Listele, ex, blt);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 02.08.2016)
        /// </summary>
        internal dynamic GetByUserId(decimal userId, CuteModel db)
        {
            dynamic result = new CuteDev.DynamicFormData();

            var meta = dalUsersMeta.ListByUserId(userId, db);

            foreach (var item in meta.Values)
            {
                result.Add(item.metaKey, item.metaValue);
            }

            return result;
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rList<rUsersMeta> ListByCurrentUserId(Bilet blt, CuteModel db)
        {
            //LogYaz("Kullanıcı ayarları listelendi. ListByCurrentUserId", blt);

            return dalUsersMeta.ListByCurrentUserId(blt, db);
        }

        /// <summary>
        /// UsersMeta listeler. (volkansendag - 02.08.2016)
        /// </summary>
        public rList<rUsersMeta> ListByCurrentUserId(Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return ListByCurrentUserId(blt, db);
                }
                catch (Exception ex)
                {
                    //bllLog.Ekle(IslemSinifi, IslemMetodlari.Listele, ex, blt);
                    throw ex;
                }
            }
        }
    }
}
