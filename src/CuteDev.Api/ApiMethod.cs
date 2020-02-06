using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Api
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApiMethod : Attribute
    {
        public bool sessionSecure = true;
        public string permissionId;

        // This constructor defines two required parameters: name and level. 
        public ApiMethod()
            : this(true)
        {

        }
        public ApiMethod(bool sessionSecure)
        {
            this.sessionSecure = sessionSecure;
        }

        public ApiMethod(string permissionId) : this(true)
        {
            this.permissionId = permissionId;
        }
    }
}
