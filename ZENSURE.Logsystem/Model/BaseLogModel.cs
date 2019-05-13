using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZENSURE.LogSystem.Enums;

namespace ZENSURE.Logsystem.Model
{
    public class BaseLogModel
    {
        /// <summary>
        /// Request Type
        /// </summary>
        public RequestTypeEnum Type { get; set; }

        /// <summary>
        /// Request Type Name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Log Type
        /// </summary>
        public string LogTypeName { get; set; }

        /// <summary>
        /// Log Source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// App
        /// </summary>
        public AppEnum App { get; set; }

        /// <summary>
        /// Request Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Request Mode
        /// </summary>
        public ModeEnum Mode { get; set; }


        /// <summary>
        /// Request Date
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// App Edition
        /// </summary>
        public string Edition { get; set; }

        /// <summary>
        /// Your customer content
        /// </summary>
        public JObject Custom { get; set; }
    }
}
