using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;

namespace CuteDev.Database
{

    public static class ConvertData
    {

        public static T SetCreatedInfo<T>(this T ent) where T : DAL.BaseModel
        {
            ent.createDate = DateTime.Now;
            ent.createIp = "127.0.0.1";
            ent.creatorId = 1;
            return ent;
        }

        /// <summary>
        /// DataRow nesnesini verilen sütun isimlerine uygun olarak hedef nesneye "T" dönüştürür.  (volkansendag - 2014.06.10)
        /// </summary>
        /// <typeparam name="T">Hedef nesne</typeparam>
        /// <param name="row">kaynak nesne</param>
        /// <param name="columnsName"></param>
        /// <returns></returns>
        public static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch { return obj; }
        }

        /// <summary>
        /// Kaynak nesneyi hedef nesneye aktarır.  (volkansendag - 28.04.2014)
        /// </summary>
        /// <typeparam name="T">Hedef nesne</typeparam>
        /// <param name="myobj">Kaynak nesne</param>
        /// <returns></returns>
        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();

            Type target = typeof(T);

            var x = Activator.CreateInstance(target, false);

            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;

            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;

            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();

            PropertyInfo propertyInfo;

            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);

                PropertyInfo desObj = myobj.GetType().GetProperty(memberInfo.Name);
                if (desObj == null)
                    continue;

                propertyInfo.SetValue(x, desObj.GetValue(myobj, null), null);
            }
            return (T)x;
        }

        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");

            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Collect all the valid properties to map
            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };

            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }

        }
    }
}
