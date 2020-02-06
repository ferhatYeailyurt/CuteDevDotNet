using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Web
{
    public class AuthenticatedUser
    {
        #region Properties

        public int Id { get; set; }

        public int Tip { get; set; }

        public string SessionId { get; set; }

        public object UserInfo { get; set; }


        #endregion

        #region Constructor

        public AuthenticatedUser()
        {
            Initialize();
        }

        public AuthenticatedUser(int id)
            : base()
        {
            Initialize();

            this.Id = id;
        }

        public AuthenticatedUser(int id, object userInfo)
            : base()
        {
            Initialize();

            this.Id = id;
            this.UserInfo = userInfo;
        }

        #endregion

        #region Functions

        private void Initialize()
        {
            this.SessionId = System.Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        }

        #endregion
    }
}
