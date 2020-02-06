/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Users;
using CuteDev.Web;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CuteDev.Users.Data.Entity.UsersMeta;
using System.Text;

namespace CuteDev.Users.Data.BLL
{
    public class bllUsers : bllBase
    {

        private const string IslemSinifi = "bllUsers";

        public void AddDefaultUsers(CuteModel db, Bilet blt = null)
        {

            if (blt == null)
                blt = new Bilet();

            var list = new List<DAL.Model.Users>();

            var ent = Get<DAL.Model.Users>();
            SetCreateValues(ent, blt);

            var pass = new Crypto(ent.guid).Encrypt("123654");
            ent.password = pass;
            ent.email = "naklov67@gmail.com";
            ent.fullname = "Admin";
            ent.role = "Admin";

            list.Add(ent);

            db.Users.AddOrUpdate(p => p.email, list.ToArray());
            db.SaveChanges();
        }

        public rValue<int> Add(pUsers prms, Bilet blt, CuteModel db)
        {
            if (prms == null)
                throw Exceptions.Parameter();

            if (prms.password.isEmpty())
                throw Exceptions.Parameter("Parola boş olamaz");

            if (prms.email.isEmpty())
                throw Exceptions.Parameter("Email boş olamaz");

            var ent = Get<DAL.Model.Users>();

            ent.email = prms.email;
            ent.fullname = prms.fullname ?? prms.email;
            ent.password = prms.password;
            ent.role = "User";

            if (Exist<dalUsers>(ent, db))
                throw Exceptions.Exist("Aynı kullanıcı daha önce kaydedilmiş.");

            ent.guid = Guid.NewGuid().ToString();
            ent.password = new Crypto(ent.guid).Encrypt(ent.password);

            Add(ent, blt, db);

            LogYaz("Kullanıcı eklendi", blt, prms, ent);

            return new rValue<int>(ent.id);
        }

        public rValue<int> Add(pUsers prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = Add(prms, blt, db);
                    trns.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }

        /// <summary>
        /// Users ekler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rCore RememberMe(pUsersSignup prms, Bilet blt, CuteModel db)
        {
            if (prms.email.isEmpty() || !prms.email.isEmailAddress())
                throw Exceptions.Parameter("E-posta adresi hatalı.");

            var ent = db.Users.SingleOrDefault(p => p.email == prms.email && !p.deleted);

            if (ent == null)
                throw Exceptions.Exist("E-posta adresi ile kayıtlı kullanıcı bulunamadı.");

            var mailSender = new Mail.EmailSender();
            mailSender.IsBodyHtml = true;

            Crypto crypto = new Crypto();
            var oldPass = new Crypto(ent.guid).Decrypt(ent.password);

            var content = new StringBuilder();

            content.AppendLine();
            content.AppendLine("Şifre hatırlatma talebiniz üzerine şifreniz mail olarak gönderilmiştir. <br />");
            content.AppendLine("Aşağıda yer alan şifrenizi kullanarak sisteme giriş yapınız ve ardından şifrenizi değiştiriniz. <br />");
            content.AppendLine("<br />");
            content.AppendLine("<br />");
            content.AppendLine("Şifreniz: " + oldPass);

            string mail = ent.email;

            mailSender.Send(new Mail.Parameters.pSend()
            {
                Content = content.ToString(),
                Subject = "Şifre Hatırlatma",
                To = mail,
            });

            LogYaz(string.Format("RememberMe metodu ile şifre mail olarak gönderildi. {0} ({1})", ent.email, ent.id), blt);

            return new rCore();
        }

        public rCore RememberMe(pUsersSignup prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return RememberMe(prms, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }

        public rValue<int> Update(pUsers prms, Bilet blt, CuteModel db)
        {
            var ent = Get<DAL.Model.Users>(prms.id, db);

            if (ent == null)
                throw Exceptions.NotExist();

            string islemOncesiVeri = ent.toJson();

            ent.fullname = prms.fullname;
            if (prms.password.isNoEmpty())
            {
                ent.password = new Crypto(ent.guid).Encrypt(prms.password);
            }

            Update(ent, blt, db);

            LogYaz("Kullanıcı güncellendi.", blt, prms, ent, islemOncesiVeri);

            return new rValue<int>(ent.id);
        }

        public rValue<int> Update(pUsers prms, Bilet blt)
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
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }


        public void SaveProfile(pUserProfile prms, Bilet blt, CuteModel db)
        {
            Update(new pUsers() { fullname = prms.fullname, id = blt.KullaniciId }, blt, db);

            new bllUsersMeta().Update(new Entity.UsersMeta.pUsersMeta()
            {
                List = prms.meta.Select(p => new pUsersMeta()
                {
                    metaKey = p.Key,
                    metaValue = p.Value
                }).ToList()
            }, blt, db);

            var info = Info(blt, db);

            new SessionManager().Login(blt.KullaniciId, info);
        }

        public void SaveProfile(pUserProfile prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    SaveProfile(prms, blt, db);
                    trns.Commit();
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }

        public rValue<int> Delete(pId prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = this.Delete<DAL.Model.Users, CuteModel>(prms, blt);
                    trns.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }

        internal rUsers GetById(decimal id, Bilet blt, CuteModel db)
        {
            rUsers result = dalUsers.GetById(id, db);

            return result;
        }

        public rUsers GetById(decimal id, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return GetById(id, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal rUsers GetByUserEmail(string email, Bilet blt, CuteModel db)
        {
            rUsers result = dalUsers.GetByUserEmail(email, db);

            return result;
        }

        public rUsers GetByUserEmail(string email, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return GetByUserEmail(email, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal rUsers GetByUserEmailAndPassword(string email, string password, Bilet blt, CuteModel db)
        {
            rUsers result = dalUsers.GetByUserEmailAndPassword(email, password, db);

            return result;
        }

        public rUsers GetByUserEmailAndPassword(string email, string password, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return GetByUserEmailAndPassword(email, password, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal rUserProfile Info(Bilet blt, CuteModel db)
        {
            var result = dalUsers.Info(blt, db);

            bllUsersMeta bllMeta = new bllUsersMeta();

            result.meta = bllMeta.GetByUserId(result.id, db);

            LogYaz("Kullanıcı listelendi.", blt);

            return result;
        }

        public rValue<rUserProfile> Info(pCore prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return new rValue<rUserProfile>(Info(blt, db));
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal int Count(Bilet blt, CuteModel db)
        {
            var result = dalUsers.Count(blt, db);

            LogYaz("Kullanıcı sayısı getirildi.", blt);

            return result;
        }

        public int Count(Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return Count(blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal rUserProfile GetCurrentUser(CuteModel db)
        {
            var sm = new SessionManager();
            var blt = sm.BiletAl(true);

            if (blt == null || blt.KullaniciId <= 0)
                return null;

            if (sm.OnlineUser != null)
            {
                var usr = (rUserProfile)sm.OnlineUser.UserInfo;

                if (usr != null)
                    return usr;
            }

            var userInfo = Info(blt, db);

            return userInfo;

        }

        public rUserProfile GetCurrentUser()
        {
            using (var db = getDb())
            {
                try
                {
                    return GetCurrentUser(db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Users ekler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<rUsers> Signup(pUsersSignup prms, Bilet blt, CuteModel db)
        {
            if (prms.email.isEmpty() || !prms.email.isEmailAddress())
                throw Exceptions.Parameter("E-posta adresi hatalı.");

            if (prms.password.isEmpty() || !prms.password.isStrongPassword())
                throw Exceptions.Parameter("Parolanız daha güçlü olmalı.");

            var guid = Guid.NewGuid().ToString();

            var pass = new Crypto(guid).Encrypt(prms.password);

            DAL.Model.Users ent = new DAL.Model.Users()
            {
                guid = guid,
                email = prms.email,
                password = pass,
                fullname = prms.fullname,
                role = "User"
            };

            if (Exist<dalUsers>(ent, db))
                throw Exceptions.Exist("Kullanıcı daha önce eklenmiş.");

            Add(ent, blt, db);

            var user = Info(new Bilet(ent.id), db);

            var token = new SessionManager().Login(ent.id, user);

            user.token = token;

            LogYaz(string.Format("Signup metodu ile kayıt olundu. {0} ({1})", ent.email, ent.id), blt);

            return new rValue<rUsers>(user);
        }

        public rValue<rUsers> Signup(pUsersSignup prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = Signup(prms, blt, db);
                    trns.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw;
                }
            }
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<rUsers> Login(pUsersLogin prms, Bilet blt, CuteModel db)
        {
            if (!prms.email.isEmailAddress())
                throw Exceptions.Parameter();

            rUsers user;

            if (prms.password.isEmpty())
                throw Exceptions.Parameter("E-posta yada parola hatalı.");

            user = GetByUserEmail(prms.email, blt, db);

            if (user == null || user.id <= 0)
                throw Exceptions.Session("E-posta yada parola hatalı.");

            var userDetail = Get<DAL.Model.Users>(user.id, db);

            var prmsPassword = new Crypto(userDetail.guid).Encrypt(prms.password);

            if (prmsPassword != userDetail.password)
                throw Exceptions.Session("E-posta yada parola hatalı.");

            var info = Info(new Bilet(user.id), db);

            var token = new SessionManager().Login(user.id, info);

            user.token = token;

            LogYaz("Oturum Açıldı. " + user.email, blt);

            return new rValue<rUsers>(user);
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        public rValue<rUsers> Login(pUsersLogin prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return Login(prms, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        internal rValue<rUsers> LoginAdmin(pUsersLogin prms, Bilet blt, CuteModel db)
        {
            if (!prms.email.isEmailAddress())
                throw Exceptions.Parameter();

            rUsers user;

            if (prms.password.isEmpty())
                throw Exceptions.Parameter("E-posta yada parola hatalı.");

            user = GetByUserEmail(prms.email, blt, db);

            if (user == null || user.id <= 0)
                throw Exceptions.Session("E-posta yada parola hatalı.");

            var userDetail = Get<DAL.Model.Users>(user.id, db);

            if (userDetail.role != "Admin")
                throw Exceptions.Session("E-posta yada parola hatalı.");

            var prmsPassword = new Crypto(userDetail.guid).Encrypt(prms.password);

            if (prmsPassword != userDetail.password)
                throw Exceptions.Session("E-posta yada parola hatalı.");

            var info = Info(new Bilet(user.id), db);

            var token = new SessionManager().Login(user.id, info);

            user.token = token;

            LogYaz("Oturum Açıldı. " + user.email, blt);

            return new rValue<rUsers>(user);
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        public rValue<rUsers> LoginAdmin(pUsersLogin prms, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return LoginAdmin(prms, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        internal rList<rUsers> List(pList prms, Bilet blt, CuteModel db)
        {
            var result = dalUsers.List(prms, db);

            LogYaz("Kullanıcı listelendi.", blt);

            return result;
        }

        public rList<rUsers> List(pList args, Bilet blt)
        {
            using (var db = getDb())
            {
                try
                {
                    return List(args, blt, db);
                }
                catch (Exception ex)
                {
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }

        public rValue<int> ChangePassword(pUserChangePassword prms, Bilet blt, CuteModel db)
        {
            var ent = Get<DAL.Model.Users>(blt.KullaniciId, db);

            if (ent == null)
                throw Exceptions.NotExist();

            var oldPass = new Crypto(ent.guid).Decrypt(ent.password);

            if (oldPass != prms.oldPassword)
                throw Exceptions.ValidPassword();

            if (prms.newPassword != prms.newPasswordCheck)
                throw Exceptions.ValidNewPassword();

            string islemOncesiVeri = ent.toJson();

            if (prms.newPassword.isNoEmpty())
            {
                ent.password = new Crypto(ent.guid).Encrypt(prms.newPassword);
            }

            Update(ent, blt, db);

            LogYaz("Kullanıcı şifresi güncellendi.", blt, prms, ent, islemOncesiVeri);

            return new rValue<int>(ent.id);
        }

        public rValue<int> ChangePassword(pUserChangePassword prms, Bilet blt)
        {
            using (var db = getDb())
            {
                var trns = db.Database.BeginTransaction();
                try
                {
                    var result = ChangePassword(prms, blt, db);
                    trns.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    LogYaz(ex, blt);
                    throw ex;
                }
            }
        }
    }
}
