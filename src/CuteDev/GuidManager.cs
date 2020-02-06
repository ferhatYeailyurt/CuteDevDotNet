using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev
{
    public static class GuidManager
    {
        public static string NewId(int len)
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, len);
        }

        public static string NewId(int len, bool upper)
        {
            if (upper)
                return Guid.NewGuid().ToString().ToUpper().Replace("-", string.Empty).Substring(0, len);
            else
                return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, len);
        }
    }
}
