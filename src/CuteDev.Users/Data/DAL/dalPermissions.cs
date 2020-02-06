/* Author: Volkan Sendag - vsendag@gmail.com */
using System.Linq;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Entity.Results;
using System.Collections.Generic;

namespace CuteDev.Users.Data.DAL
{

    /// <summary>
    /// Permissions üzerinde işlem yapar. (volkansendag - 24.02.2018)
    /// </summary>
    public class dalPermissions : dalBase
    {

        /// <summary>
        /// Permissions olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public bool HasPermission(decimal permissionId, decimal userId, CuteModel db)
        {
            var roleList = db.UsersRoles.Where(p => p.user_Id == userId).Select(p => p.role_Id).ToList();

            return db.RolesPermissions.Where(p => roleList.Contains(p.rol_Id) && p.permission_Id == permissionId).Any();
        }

        /// <summary>
        /// Permissions olup olmadığını kontrol eder. (volkansendag - 24.02.2018)
        /// </summary>
        public bool HasPermissions(List<decimal> permissionId, decimal userId, CuteModel db)
        {
            var roleList = db.UsersRoles.Where(p => p.user_Id == userId).Select(p => p.role_Id).ToList();

            return db.RolesPermissions.Where(p => roleList.Contains(p.rol_Id) && permissionId.Contains(p.permission_Id)).Any();
        }

    }
}

