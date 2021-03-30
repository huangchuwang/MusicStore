using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NetMvcMusicStory.Admin.Unity.StrFormat
{
    public static class StringFormat
    {
        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：去除api返回数据中的反斜杠
        *  return：json
        */
        public static string SF(string data)
        {
            object json = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(data)));
            return System.Convert.ToString(json);
        }

    }
}