using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace CuteDev
{
    public static class Extensions
    {

        public static string BytesToString(this int byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }


        public static string FormatWith(this string format, object source, bool doubleBracket = false)
        {
            if (doubleBracket)
            {
                return FormatDoubleBracketWith(format, null, source);
            }
            else
            {
                return FormatWith(format, null, source);
            }
        }

        public static string FormatDoubleBracketWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            Regex r = new Regex(@"(?<start>\{\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\}\})+",
              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate (Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];

                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                  + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }

        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate (Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];

                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                  + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }

        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string ToTitleCase(this string str, string culture)
        {
            TextInfo myTI = new CultureInfo(culture, true).TextInfo;
            str = myTI.ToLower(str);
            return myTI.ToTitleCase(str);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string toJson(this object obj)
        {
            var js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            try
            {

                return js.Serialize(obj);
            }
            catch (Exception)
            {
                try
                {
                    return js.Serialize(obj);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string toJson(this object obj, bool format)
        {
            try
            {
                if (format)
                {
                    return new JSonPresentationFormatter().Format(obj.toJson());
                }
                else
                {
                    return obj.toJson();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Converts json data to object
        /// </summary>
        /// <typeparam name="T">Value to be converted type</typeparam>
        /// <param name="val">Value to be converted</param>
        /// <returns>Converted T value</returns>
        /// <edit date="" sign=""></edit>
        //[System.Diagnostics.DebuggerStepThrough]
        public static T toObjectFromJson<T>(this string val) where T : class
        {
            var js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };

            try
            {
                return js.Deserialize<T>(val);
            }
            catch (Exception)
            {
                try
                {
                    return js.Deserialize<T>(val);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Converts json data to object
        /// </summary>
        /// <typeparam name="T">Value to be converted type</typeparam>
        /// <param name="val">Value to be converted</param>
        /// <returns>Converted T value</returns>
        /// <edit date="" sign=""></edit>
        //[System.Diagnostics.DebuggerStepThrough]
        public static object toObjectFromJson(this string val, object destObj)
        {
            if (String.IsNullOrEmpty(val))
                return destObj;
            var js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            var source = js.Deserialize<Dictionary<string, object>>(val);

            Type someObjectType = destObj.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                var prop = someObjectType.GetProperty(item.Key);

                if (prop != null)
                {
                    var _val = Convert.ChangeType(item.Value, prop.PropertyType);

                    prop.SetValue(destObj, _val, null);
                }
            }

            return destObj;
        }

        public static T toObjectFromFormData<T>(this string val) where T : class
        {
            try
            {
                var outgoingQueryString = HttpUtility.ParseQueryString(val);
                var t = outgoingQueryString.Keys.Cast<string>()
                    .ToDictionary(k => k, k => outgoingQueryString[k]);

                var tt = t.toJson();
                var result = tt.toObjectFromJson<T>();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static string getMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static DateTime? toDateTime(this object obj)
        {
            DateTime? result = null;

            try
            {
                if (obj != null && obj != DBNull.Value)
                    result = Convert.ToDateTime(obj);
            }
            catch { }

            return result;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static DateTime toDateTimeDefault(this object obj)
        {
            DateTime result = new DateTime(1900, 1, 1);

            try
            {
                if (obj != null && obj != DBNull.Value)
                    result = Convert.ToDateTime(obj);
            }
            catch { }

            return result;
        }

        /// <summary>
        /// To the json default.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static string toJsonDefault(this object val)
        {
            return val.toJsonDefault("{}");
        }

        /// <summary>
        /// To the json default.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static string toJsonDefault(this object val, string defaultValue)
        {
            try
            {
                return val.toJson();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Kelime sayısını getirir
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static int CountWords(this string test)
        {
            if (test.isEmpty())
                return 0;

            int count = 0;
            bool wasInWord = false;
            bool inWord = false;

            for (int i = 0; i < test.Length; i++)
            {
                if (inWord)
                {
                    wasInWord = true;
                }

                if (Char.IsWhiteSpace(test[i]))
                {
                    if (wasInWord)
                    {
                        count++;
                        wasInWord = false;
                    }
                    inWord = false;
                }
                else
                {
                    inWord = true;
                }
            }

            // Check to see if we got out with seeing a word
            if (wasInWord)
            {
                count++;
            }

            return count;
        }


        /// <summary>
        /// Checks empty value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns>If value is empty then returns true</returns>
        /// <edit date="" sign=""></edit>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool isEmpty(this string val)
        {
            return val == String.Empty || String.IsNullOrEmpty(val) || val.Replace(" ", "")
                                                                          .Replace("\t", "")
                                                                          .Replace("\r", "")
                                                                          .Replace("\n", "") == "" ? true : false;
        }

        /// <summary>
        /// Checks empty value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns>If value is empty then returns true</returns>
        /// <edit date="" sign=""></edit>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool isNoEmpty(this string val)
        {
            return !val.isEmpty();
        }

        /// <summary>
        /// akizilgil - 01.09.2011
        /// Girilen değerin E-posta adresi formatının doğru olup olmadığını kontrol eder
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool isEmailAddress(this string str)
        {

            if (str.isEmpty())
                return false;

            // /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/
            // ^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (System.Text.RegularExpressions.Regex.IsMatch(str, pattern))
                return true;

            return false;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static bool isUrl(this string str)
        {
            // /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/
            // ^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$
            string pattern = @"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)";

            if (System.Text.RegularExpressions.Regex.IsMatch(str, pattern))
                return true;

            return false;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static bool isStrongPassword(this string password, int targetPoint = 50)
        {
            int lengthScore = Math.Min(10, password.Length) * 6;
            int lowerScore = Math.Min(2, (password.Length - Regex.Replace(password, "[a-z]", "").Length)) * 5;
            int upperScore = Math.Min(2, (password.Length - Regex.Replace(password, "[A-Z]", "").Length)) * 5;
            int digitScore = Math.Min(2, (password.Length - Regex.Replace(password, "[0-9]", "").Length)) * 5;
            int symbolScore = Math.Min(2, (Regex.Replace(password, "[a-zA-Z0-9]", "").Length)) * 5;

            return (lengthScore + lowerScore + upperScore + digitScore + symbolScore) > targetPoint;
        }


        [System.Diagnostics.DebuggerStepThrough]
        public static string toMD5(this string str)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string toMD5(this string str, bool? isUpper)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                if (isUpper == null || isUpper.Value == false)
                    sb.Append(hashBytes[i].ToString("x2"));
                else
                    sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string removeTurkishChars(this string str)
        {
            if (str == null) return str;

            str = str.Replace("Ü", "U").Replace("Ğ", "G").Replace("İ", "I").Replace("Ş", "S").Replace("Ç", "C").Replace("Ö", "O");
            str = str.Replace("ü", "u").Replace("ğ", "g").Replace("ı", "i").Replace("ş", "s").Replace("ç", "c").Replace("ö", "o");

            return str;
        }

        public static string getLeft(this string str, int maxLength)
        {
            if (String.IsNullOrEmpty(str)) return str;

            maxLength = Math.Abs(maxLength);

            return (str.Length <= maxLength
                   ? str
                   : str.Substring(0, maxLength)
                   );
        }

        public static string convertToUrlString(this string str)
        {
            if (String.IsNullOrEmpty(str)) return str;
            str = str.Trim().removeHTMLTags(true).removeTurkishChars();
            str = Regex.Replace(str, "[^A-Za-z0-9 _]", "");
            str = str.Replace("     ", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Replace(" ", "_");
            str = str.ToLower().Replace("ı", "i");
            return str;
        }

        public static string convertToUrlString(this string str, int maxLength)
        {
            return str.convertToUrlString().getLeft(maxLength);
        }


        [System.Diagnostics.DebuggerStepThrough]
        public static string removeHTMLTags(this string val, bool decode)
        {
            string result = Regex.Replace(val, @"<(.|\n)*?>", String.Empty);

            return decode ? System.Web.HttpUtility.HtmlDecode(result) : result;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> q, string sort, string sortType)
        {
            var classPara = Expression.Parameter(typeof(T), "t");
            var pi = typeof(T).GetProperty(sort);
            q = q.Provider.CreateQuery<T>(
                                Expression.Call(
                                    typeof(Queryable),
                                    sortType == "asc" ? "OrderBy" : "OrderByDescending",
                                    new Type[] { typeof(T), pi.PropertyType },
                                    q.Expression,
                                    Expression.Lambda(Expression.Property(classPara, pi), classPara))
                                );
            return q;
        }


        #region DateTime

        [System.Diagnostics.DebuggerStepThrough]
        public static long dateTimeToLong(this DateTime val)
        {
            StringBuilder result = new StringBuilder();
            result.Append(val.Year);
            result.Append(String.Format("{0:00}", val.Month));
            result.Append(String.Format("{0:00}", val.Day));
            result.Append(String.Format("{0:00}", val.Hour));
            result.Append(String.Format("{0:00}", val.Minute));
            result.Append(String.Format("{0:00}", val.Second));
            result.Append(String.Format("{0:000}", val.Millisecond));

            return int.Parse(result.ToString());
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static int lastDayOfMonth(this DateTime val)
        {
            if (val.Month == 1 || val.Month == 3 || val.Month == 5 || val.Month == 7 || val.Month == 8 || val.Month == 10 || val.Month == 12)
            {
                return 31;
            }
            else if (val.Month == 2)
            {
                if (val.Year % 4 == 0 && (val.Year % 100 != 0 || val.Year % 400 == 0))
                    return 29;
                else
                    return 28;
            }

            return 30;
        }

        /// <summary>
        /// gecerli bir tarihsaat nesnesi olup olmadigini dondurur. (volkansendag - 13.01.2016)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool isDateTime(this object val)
        {
            if (val.GetType() == typeof(DateTime) || val.GetType() == typeof(DateTime?))
                return true;

            if (val == null || val.ToString().isEmpty())
                return false;

            DateTime dateResult;

            DateTime.TryParse(val.ToString(), out dateResult);

            return (dateResult != null);
        }

        /// <summary>
        /// gecerli bir tarih nesnesi olup olmadigini dondurur. (volkansendag - 13.01.2016)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool isDate(this object val)
        {
            if (!val.isDateTime())
                return false;

            DateTime dateResult;

            DateTime.TryParse(val.ToString(), out dateResult);

            return (dateResult.startOfDay() == dateResult);
        }


        /// <summary>
        /// Tarihteki gunun baslangic zamanini verir. (volkansendag - 13.01.2016)
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static DateTime startOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        /// <summary>
        /// Tarihteki gunun bitis zamanini verir. (volkansendag - 13.01.2016)
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static DateTime endOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// iki tarih arasındaki zamani sözel olarak yazar. (volkansendag - 2016.02.03)
        /// </summary>
        /// <param name="ilk"></param>
        /// <param name="son"></param>
        /// <returns></returns>
        public static string farkSuresi(this DateTime ilk, DateTime son, bool kisalt)
        {
            string result = "";
            var gun = kisalt ? " g," : " gün,";
            var dakika = kisalt ? " dk," : " dakika,";
            var saniye = kisalt ? " sn" : " saniye";

            TimeSpan span = (son - ilk);
            if (span.Days > 0)
                result += span.Days + gun;

            if (span.Hours > 0)
                result += span.Hours + " saat, ";

            if (span.Minutes > 0)
                result += span.Minutes + dakika;

            result += span.Seconds + saniye;

            return result;
        }

        public static string farkSuresi(this DateTime ilk, DateTime son)
        {
            return ilk.farkSuresi(son, false);
        }

        public static string farkSuresi(this DateTime ilk)
        {
            return ilk.farkSuresi(DateTime.Now, false);
        }

        public static string farkSuresi(this DateTime ilk, bool kisalt)
        {
            return ilk.farkSuresi(DateTime.Now, kisalt);
        }

        #endregion

        #region Convert Type

        public static object GetPropValue(this object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static object TryTypeConvert(this object p, Type type)
        {
            if (type == typeof(Boolean))
            {
                Boolean result = new Boolean();
                result = (p.ToString() == "True");
                return result;
            }

            if (type == typeof(Decimal?) && p.GetType() == typeof(string))
            {
                Decimal outValue;
                return Decimal.TryParse(p.ToString(), out outValue) ? (Decimal?)outValue : null;
            }

            if (type == typeof(Decimal) && p.GetType() == typeof(string))
            {
                return Decimal.Parse(p.ToString());
            }

            if (type == typeof(Int32) && p.GetType() == typeof(string))
            {
                return Int32.Parse(p.ToString());
            }

            if (type == typeof(Int32?) && p.GetType() == typeof(string))
            {
                int outValue;
                return int.TryParse(p.ToString(), out outValue) ? (int?)outValue : null;
            }

            if (type == typeof(Int64) && p.GetType() == typeof(string))
            {
                return Int64.Parse(p.ToString());
            }

            if (type == typeof(Byte) && p.GetType() == typeof(string))
            {
                return Byte.Parse(p.ToString());
            }

            if (type == typeof(DateTime?) && p.GetType() == typeof(string))
            {
                DateTime outValue;
                return DateTime.TryParse(p.ToString(), out outValue) ? (DateTime?)outValue : null;
            }

            if (type == typeof(DateTime?) && p.GetType() == typeof(DateTime))
            {
                DateTime outValue;
                return DateTime.TryParse(p.ToString(), out outValue) ? (DateTime?)outValue : null;
            }

            if (type == typeof(DateTime) && p.GetType() == typeof(string))
            {
                return DateTime.Parse(p.ToString());
            }

            return p;
        }

        #endregion

        public static T GetResultFromWeb<T>(string url) where T : class, new()
        {
            using (var webClient = new WebClient())
            {
                var json_data = string.Empty;
                try
                {
                    webClient.Encoding = Encoding.UTF8;
                    json_data = webClient.DownloadString(url);
                }
                catch (Exception) { }

                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? json_data.toObjectFromJson<T>() : new T();
            }
        }
    }
}