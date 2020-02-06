/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Roles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;


namespace CuteDev.Users.Data.BLL
{
    public class bllRolesPermissions : bllBase
    {

        private const string IslemSinifi = "bllRolesPermissions";

        private dalUsersPermissions dal = new dalUsersPermissions();
        bllRoles rol = new bllRoles();


        public void AddDefaultRolesPermissions(CuteModel db, Bilet blt = null)
        {
            if (blt == null)
                blt = new Bilet();

            var list = new List<RolesPermissions>();

            var role = rol.GetByTitle("Admin", blt, db);
            var permList = db.Permissions.Select(p => p.id).ToList();

            foreach (var item in permList)
            {
                var ent = Get<RolesPermissions>();
                SetCreateValues(ent, blt);

                ent.permission_Id = item;
                ent.rol_Id = role.id;

                list.Add(ent);
            }

            db.RolesPermissions.AddOrUpdate(p => p.permission_Id, list.ToArray());

            db.SaveChanges();
        }

        public void AddDefaultRolesPermissions()
        {
            using (var db = getDb())
            {
                try
                {
                    AddDefaultRolesPermissions(db);
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
