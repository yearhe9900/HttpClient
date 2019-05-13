using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZENSURE.LogSystem.Enums;

namespace ZENSURE.Logsystem.Model
{
    /// <summary>
    /// System Log Model 
    /// </summary>
    public class SystemLogModel : BaseLogModel
    {
        /// <summary>
        /// Log level
        /// </summary>
        public LevelEnum Level { get; set; }

        /// <summary>
        /// Log message
        /// </summary>
        public string Message { get; set; }
    }
}
