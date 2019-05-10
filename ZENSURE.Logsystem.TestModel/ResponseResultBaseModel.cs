using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZENSURE.Logsystem.TestModel
{
    public class ResponseResultBaseModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        public object Data { get; set; }
    }
}
