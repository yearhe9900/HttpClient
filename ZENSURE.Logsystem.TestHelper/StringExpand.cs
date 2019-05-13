using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem
{
    public static class StringExpand
    {
        public static T ToData<T>(this string input) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Determine whether a string is a URL
        /// </summary>
        /// <param name="url">Url String</param>
        /// <returns></returns>
        public static bool IsUrl(string url)
        {
            try
            {
                string urlMatch = @"[a-zA-z]+://[^\s]*";

                return Regex.IsMatch(url, urlMatch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// MD5_32
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToMD5With32(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)));
            t2 = t2.Replace("-", "");
            return t2.ToUpper();
        }

        /// <summary>
        /// Get current unix timestamp by current datetime
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimeStampByDatetime(DateTime dateTime)
        {
            var epoch = (dateTime.Ticks - 621355968000000000) / 10000000;//时间转Unix时间戳
            return epoch;
        }

        /// <summary>
        /// Get Timestamp And Sign
        /// </summary>
        /// <returns></returns>
        public static (string timestamp, string sign) GetTimestampAndSign()
        {
            var epoch = GetUnixTimeStampByDatetime(DateTime.Now.ToUniversalTime());

            var sign = StrToMD5With32(epoch + "zlead");

            return (epoch.ToString(), sign);
        }
    }
}
