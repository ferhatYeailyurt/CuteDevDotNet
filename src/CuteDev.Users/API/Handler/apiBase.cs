using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuteDev.Users.Data.BLL;

namespace CuteDev.Users.API.Handler
{
    public class apiBase : CuteDev.Api.apiBase  //apiAuthenticatedBase
    {
        private bllUsersPermissions bllPerm = new bllUsersPermissions();

        public override bool CheckPermission(Bilet blt)
        {
            if (this.MetodInfo.permissionId.isEmpty())
                return true;

            var perm = this.MetodInfo.permissionId;

            return bllPerm.HasPermission(perm, blt.KullaniciId);
        }
    }
}
