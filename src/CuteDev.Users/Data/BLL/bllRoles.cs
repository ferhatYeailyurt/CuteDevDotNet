/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Roles;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CuteDev.Users.Data.BLL
{
    public class bllRoles : bllBase
    {

        private const string IslemSinifi = "bllRoles";

        private dalUsersPermissions dal = new dalUsersPermissions();


        public void AddDefaultRoles(CuteModel db, Bilet blt = null)
        {
            if (blt == null)
                blt = new Bilet();

            var list = new List<Roles>();
            var entRol = Get<Roles>();
            SetCreateValues(entRol, blt);
            entRol.title = "Admin";
            entRol.description = "Administrator";

            list.Add(entRol);

            db.Roles.AddOrUpdate(p => p.title, list.ToArray());

            db.SaveChanges();

            var entUser = db.Users.FirstOrDefault(p => p.email == "naklov67@gmail.com");

            var userRole = new UsersRoles()
            {
                user_Id = entUser.id,
                role_Id = entRol.id
            };

            SetCreateValues(userRole, blt);

            db.UsersRoles.AddOrUpdate(p => p.user_Id, userRole);

            db.SaveChanges();
        }

        internal rRoles GetById(decimal id, Bilet blt, CuteModel db)
        {
            if (id <= 0)
                throw Exceptions.Parameter();

            //LogYaz("Kullanıcı ayarları getirildi.", blt);

            return dalRoles.GetById(id, db);
        }

        internal rRoles GetByTitle(string title, Bilet blt, CuteModel db)
        {
            if (title.isEmpty())
                throw Exceptions.Parameter();

            //LogYaz("Kullanıcı ayarları getirildi.", blt);

            return dalRoles.GetByTitle(title, db);
        }
    }
}
