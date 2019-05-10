using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem
{
    public static class StringExpand
    {
        public static T ToData<T>(this string input) where T:class
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
    }
}
