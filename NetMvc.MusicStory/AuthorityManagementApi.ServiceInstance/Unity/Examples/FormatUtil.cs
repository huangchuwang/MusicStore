using Newtonsoft.Json;
using System.Collections;

namespace AuthorityManagementApi.ServiceInstance.MicroService.ServiceInstance.Unity
{


    public static class FormatUtil
    {
        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：Json格式化工具方法
        *  return：json
        */
        public static string JsonFormat(string msg, int statu, string token,string user,string icon,string name)
        {
            Hashtable tab = new Hashtable();
            tab.Add("msg",msg);
            tab.Add("code",statu);
            tab.Add("token",token);
            tab.Add("user", user);
            tab.Add("icon", icon);
            tab.Add("name", name);
            return JsonConvert.SerializeObject(tab);
        }


        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：Json格式化工具方法
        *  return：json
        */
        public static string Status(string msg, int statu)
        {
            Hashtable tab = new Hashtable();
            tab.Add("msg", msg);
            tab.Add("code", statu);
            return JsonConvert.SerializeObject(tab);
        }

        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：Json格式化工具方法
        *  return：json
        */
        public static string Result(int count,string msg, int statu,object data)
        {
            Hashtable tab = new Hashtable();
            tab.Add("count", count);
            tab.Add("msg", msg);
            tab.Add("code", statu);
            tab.Add("data", data);
            return JsonConvert.SerializeObject(tab);
        }
    }
}