using Newtonsoft.Json;
using System.Collections;

namespace PuApi.MicroService.ServiceInstance.Unity
{
    public static class FormatUtil
    {
        public static string JsonFormat(int count,string msg, int statu, object data)
        {
            Hashtable tab = new Hashtable();
            tab.Add("count", count);
            tab.Add("msg",msg);
            tab.Add("code",statu);
            tab.Add("data",data);
            return JsonConvert.SerializeObject(tab);
        }
    }
}