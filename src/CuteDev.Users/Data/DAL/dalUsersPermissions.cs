/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Entity.Results;
using System.Collections.Generic;

namespace CuteDev.Users.Data.DAL
{

    /// <summary>
    /// UsersPermissions üzerinde işlem yapar. (volkansendag - 24.02.2018)
    /// </summary>
    public class dalUsersPermissions : dalBase
    {

        /// <summary>
        /// UsersPermissions olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public bool HasPermission(string permissionCode, decimal userId, CuteModel db)
        {
            var permissionId = db.Permissions.Where(p => !p.deleted && p.code == permissionCode).Select(p => p.id).FirstOrDefault();

            if (permissionId <= 0)
                return false;

            var roleList = db.UsersRoles.Where(p => !p.deleted && p.user_Id == userId).Select(p => p.role_Id).ToList();

            return db.RolesPermissions.Where(p => !p.deleted && roleList.Contains(p.rol_Id) && p.permission_Id == permissionId).Any();
        }

        /// <summary>
        /// UsersPermissions olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public bool HasPermissions(List<decimal> permissionId, decimal userId, CuteModel db)
        {
            var roleList = db.UsersRoles.Where(p => !p.deleted && p.user_Id == userId).Select(p => p.role_Id).ToList();

            return db.RolesPermissions.Where(p => !p.deleted && roleList.Contains(p.rol_Id) && permissionId.Contains(p.permission_Id)).Any();
        }
    }
}

