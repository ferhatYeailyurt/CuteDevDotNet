/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev.Api;
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Users.Data.BLL;
using CuteDev.Users.Data.Entity.UsersMeta;
using System.Collections.Generic;

namespace CuteDev.Users.API.Handler
{

    /// <summary>
    /// Users üzerinde işlem yapar. (volkansendag - 02.08.2016)
    /// </summary>
    public class apiUsersMeta : apiBase //apiAuthenticatedBase
    {
        private bllUsersMeta bll = new bllUsersMeta();

        /// <summary>
        /// Users gunceller. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod]
        public rValue<rCore> update()
        {
            return bll.Update(this.getParams<pUsersMeta>(), this.BiletAl());
        }

        /// <summary>
        /// Users listeler. (volkansendag - 02.08.2016)
        /// </summary>
        [ApiMethod]
        public rList<rUsersMeta> list()
        {
            return bll.ListByCurrentUserId(this.BiletAl());
        }
    }
}
