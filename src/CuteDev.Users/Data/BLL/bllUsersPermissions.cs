/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.DAL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Web;
using System;
using System.Data.Entity.Migrations;


namespace CuteDev.Users.Data.BLL
{
    public class bllUsersPermissions : bllBase
    {

        private const string IslemSinifi = "bllUsersPermissions";

        private dalUsersPermissions dal = new dalUsersPermissions();

        internal bool HasPermission(string permissionId, decimal userId, CuteModel db)
        {
            var result = dal.HasPermission(permissionId, userId, db);

            return result;
        }

        public bool HasPermission(string permissionId, decimal userId)
        {
            using (var db = getDb())
            {
                try
                {
                    return HasPermission(permissionId, userId, db);
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
