/* Author: Volkan Şendağ - volkansendag@belsis.com.tr */
using System;
using System.Linq;
using CuteDev.Database;
using CuteDev.Entity.Results;
using CuteDev.Entity.Parameters;
using CuteDev.Database.DAL;

namespace CuteDev.Database.BLL
{

    /// <summary>
    /// Ortak BLL işlemleri
    /// </summary>
    public class bllBase
    {
        public virtual ProcessException getEx(Exception ex)
        {

            var message = ex.Message;

            if (ex.GetType().Name == "DbEntityValidationException")
            {
                var errors = ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors;
                if (errors.Count() > 0)
                    message = string.Empty;

                foreach (var err in errors)
                {
                    message += string.Join(",", err.ValidationErrors.Select(p => p.ErrorMessage).ToList());
                }
            }
            else if (ex.InnerException != null)
            {
                message = ex.InnerException.Message;
            }

            return new ProcessException(message);
        }

        internal static T getDb<T>() where T : CuteModel, new()
        {
            return new T();
        }

        public T Get<T>(int? id, CuteModel db) where T : BaseModel, new()
        {
            return dalBase.Get<T>(id, db);
        }

        public T Get<T>() where T : BaseModel, new()
        {
            return dalBase.Get<T>();
        }

        public bool Exist<T>(BaseModel ent, CuteModel db) where T : dalBase, new()
        {
            var dal = new T();
            return dal.Exist(ent, db);
        }

        public bool Exist<T>(BaseModel ent, Bilet blt, CuteModel db) where T : dalBase, new()
        {
            var dal = new T();
            return dal.Exist(ent, blt, db);
        }

        public void SetUpdateValues(BaseModel ent, Bilet blt)
        {
            ent.updaterId = blt.KullaniciId;
            ent.updateIp = blt.IP;
            ent.updateDate = DateTime.Now;
        }

        public void SetCreateValues(BaseModel ent, Bilet blt)
        {
            if (ent.guid.isEmpty())
                ent.guid = Guid.NewGuid().ToString();

            ent.creatorId = blt.KullaniciId;
            ent.createIp = blt.IP;
            ent.createDate = DateTime.Now;
            ent.deleted = false;
        }


        public void Add(BaseModel ent, Bilet blt, CuteModel db)
        {
            SetCreateValues(ent, blt);
            dalBase.Add(ent, db);
        }

        public void Update(BaseModel ent, Bilet blt, CuteModel db)
        {
            SetUpdateValues(ent, blt);
            dalBase.Update(ent, db);
        }

        public void Delete(BaseModel ent, Bilet blt, CuteModel db)
        {
            ent.updaterId = blt.KullaniciId;
            ent.updateIp = blt.IP;
            ent.updateDate = DateTime.Now;
            ent.deleted = true;

            dalBase.Delete(ent, db);
        }

        public rValue<int> Delete<T>(pId prms, Bilet ticket, CuteModel db) where T : BaseModel, new()
        {
            var ent = Get<T>(prms.id, db);

            if (ent == null)
                throw Exceptions.NotExist();

            Delete(ent, ticket, db);

            return new rValue<int>(ent.id);
        }

        public rValue<int> Delete<T, T2>(pId prms, Bilet ticket) where T : BaseModel, new() where T2 : CuteModel, new()
        {
            using (var db = getDb<T2>())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = Delete<T>(prms, ticket, db);

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

        public rValue<int> Delete<T, T2>(pIds prms, Bilet ticket) where T : BaseModel, new() where T2 : CuteModel, new()
        {
            using (var db = getDb<T2>())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    rValue<int> result = new rValue<int>();

                    if (prms.ids != null && prms.ids.Count > 0)
                    {
                        foreach (var Id in prms.ids)
                        {
                            var parameter = new pId(Id);
                            result = Delete<T>(parameter, ticket, db);
                        }
                    }
                    else if (prms.id.HasValue)
                    {
                        var parameter = new pId(prms.id.Value);
                        result = Delete<T>(parameter, ticket, db);
                    }
                    else
                    {
                        throw Exceptions.Parameter();
                    }

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
    }
}
