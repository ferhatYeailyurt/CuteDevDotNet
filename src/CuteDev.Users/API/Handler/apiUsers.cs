/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Api;
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.BLL;
using CuteDev.Users.Data.DAL.Model;
using CuteDev.Users.Data.Entity.Users;

namespace CuteDev.Users.API.Handler
{

    /// <summary>
    /// Users üzerinde işlem yapar. (volkansendag - 02.08.2016)
    /// </summary>
    public class apiUsers : apiBase //apiAuthenticatedBase
    {
        private bllUsers bll = new bllUsers();

        /// <summary>
        /// Users ekler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(PermissionList.UsersAdd)]
        public rValue<int> add()
        {
            return bll.Add(this.getParams<pUsers>(), this.BiletAl());
        }

        /// <summary>
        /// Users gunceller. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(PermissionList.UsersUpdate)]
        public rValue<int> update()
        {
            return bll.Update(this.getParams<pUsers>(), this.BiletAl());
        }

        /// <summary>
        /// Users siler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(PermissionList.UsersDelete)]
        public rValue<int> delete()
        {
            return bll.Delete<Data.DAL.Model.Users, Data.DAL.Model.CuteModel>(this.getParams<pId>(), this.BiletAl());
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod]
        public rValue<rUserProfile> info()
        {
            return bll.Info(this.getParams<pCore>(), this.BiletAl());
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(PermissionList.UsersList)]
        public rList<rUsers> list()
        {
            return bll.List(this.getParams<pList>(), this.BiletAl());
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(false)]
        public rValue<rUsers> login()
        {
            return bll.Login(this.getParams<pUsersLogin>(), this.BiletAl(false));
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod(false)]
        public rValue<rUsers> signup()
        {
            return bll.Signup(this.getParams<pUsersSignup>(), this.BiletAl(false));
        }
    }
}
