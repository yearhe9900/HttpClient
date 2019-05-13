using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem.Utils
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5_16
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string StrToMD5With16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(ConvertString)), 4, 8);
            return t2.Replace("-", "").ToUpper();
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
    }
}

