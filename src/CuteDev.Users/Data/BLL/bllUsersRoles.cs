/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;


namespace CuteDev.Users.Data.BLL
{
    public class bllUsersRoles : bllBase
    {

        private const string IslemSinifi = "bllUsersRoles";

        bllUsers usr = new bllUsers();
        bllRoles rol = new bllRoles();

        public void AddDefaultUsersRoles(CuteModel db, Bilet blt = null)
        {
            if (blt == null)
                blt = new Bilet();

            var list = new List<UsersRoles>();
            var ent = Get<UsersRoles>();
            SetCreateValues(ent, blt);

            var user = usr.GetByUserEmail("naklov67@gmail.com", blt, db);
            var role = rol.GetByTitle("Admin", blt, db);

            ent.role_Id = role.id;
            ent.user_Id = user.id;

            list.Add(ent);

            db.UsersRoles.AddOrUpdate(p => p.role_Id, list.ToArray());

            db.SaveChanges();
        }
    }
}
