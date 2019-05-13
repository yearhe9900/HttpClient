using System;
using System.Collections.Generic;
using System.Text;

namespace ZENSURE.Logsystem.TestModel
{
    public class TestStaticString
    {
        public readonly static string _getLegalUrl = "http://192.168.2.115:23649/api/values";

        public readonly static string _getNotFoundUrl = "http://192.168.2.115:23649/api/value";

        public readonly static string _postLegalUrl = "http://192.168.2.115:23649/api/UploadLogs/SendSysLogs";

        public readonly static string _jsonData = @"{
    'source': 'zlead',
    'host': '192.168.1.1',
    'app': 1,
    'type': 2, 
    'url': '/v2/employeeInfo/archives',
    'mode': 1,
    'level': 1,
    'edition': 'v1.88.3',
    'message': '这是测试出来的数据',
}";
    }
}
