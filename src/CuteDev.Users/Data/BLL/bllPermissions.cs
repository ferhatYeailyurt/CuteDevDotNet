/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Web;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CuteDev.Api;

namespace CuteDev.Users.Data.BLL
{
    public class bllPermissions : bllBase
    {

        private const string IslemSinifi = "bllPermissions";

        private dalPermissions dal = new dalPermissions();

        public void AddDefaultPermissions(CuteModel db, Bilet blt = null)
        {
            if (blt == null)
                blt = new Bilet();

            var listAttr = new PermissionList().GetList();

            var list = new List<Permissions>();

            foreach (var item in listAttr)
            {
                var ent = Get<Permissions>();
                SetCreateValues(ent, blt);
                ent.code = item.Code;
                ent.title = item.Title;
                ent.description = item.Description;
                list.Add(ent);
            }


            db.Permissions.AddOrUpdate(p => p.code, list.ToArray());

            db.SaveChanges();
        }

        public void AddDefaultPermissions(List<PermissionDetailAttribute> permList, CuteModel db, Bilet blt)
        {
            if (blt == null)
                blt = new Bilet();

            var list = new List<Permissions>();

            foreach (var item in permList)
            {
                var ent = Get<Permissions>();
                SetCreateValues(ent, blt);
                ent.code = item.Code;
                ent.title = item.Title;
                ent.description = item.Description;
                list.Add(ent);
            }

            db.Permissions.AddOrUpdate(p => p.code, list.ToArray());

            db.SaveChanges();
        }

        public void AddDefaultPermissionsNew(List<PermissionDetailAttribute> permList, Bilet blt = null)
        {
            using (var db = getDb())
            {
                try
                {
                    AddDefaultPermissions(permList, db, blt);
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
