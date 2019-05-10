using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static T ToJson<T>(this string input) where T : class
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
    }
}
