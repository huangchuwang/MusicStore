using Newtonsoft.Json;
using System.Collections;

namespace NetMvcMusicStory.Client.Unity
{


    public static class FormatUtil
    {
        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：Json格式化工具方法
        *  return：json
        */
        public static string JsonFormat(int count,string msg, int statu, object data)
        {
            Hashtable tab = new Hashtable();
            tab.Add("count", count);
            tab.Add("msg",msg);
            tab.Add("code",statu);
            tab.Add("data",data);
            return JsonConvert.SerializeObject(tab);
        }



        /*
        *  name:hcw
        *  time: 2020/1/2
        *  content：Json格式化工具方法
        *  return：json
        */
        public static string LoginResult(string msg, int statu, object icon,string name)
        {
            Hashtable tab = new Hashtable();
            tab.Add("msg", msg);
            tab.Add("code", statu);
            tab.Add("icon", icon);
            tab.Add("name", name);
            return JsonConvert.SerializeObject(tab);
        }
    }
}