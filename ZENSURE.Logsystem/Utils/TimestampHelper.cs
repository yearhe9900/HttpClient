using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem.Utils
{
   public class TimestampHelper
    {
        /// <summary>
        /// Get current unix timestamp by current datetime
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimeStampByDatetime(DateTime dateTime)
        {
            var epoch = (dateTime.Ticks - 621355968000000000) / 10000000;//Datetime To Unix
            return epoch;
        }
    }
}
