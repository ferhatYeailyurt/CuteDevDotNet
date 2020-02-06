using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Api
{
    public  class PermissionListBase
    {
        public List<PermissionDetailAttribute> GetList()
        {
            var typ = this.GetType();

            FieldInfo[] fieldInfos = typ.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            var list = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            var result = new List<PermissionDetailAttribute>();
            foreach (FieldInfo item in list)
            {
                var attrs = item.GetCustomAttributes(typeof(PermissionDetailAttribute), true);

                var info = (PermissionDetailAttribute)attrs.FirstOrDefault();

                if (info.Code.isEmpty())
                    info.Code = item.Name;

                if (info.Description.isEmpty())
                    info.Description = info.Title;

                result.Add(info);
            }

            return result;
        }

    }
}
