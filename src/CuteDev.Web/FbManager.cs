using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Web
{
    public class FbManager
    {
        public FacebookClient FB;

        public FbManager(string token)
        {
            this.FB = new FacebookClient(token);
        }
    }
}
