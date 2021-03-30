using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace NetMvcMusicStory.Admin.Unity.Invoke
{
    public static class Invoke 
    {
        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：Get请求方式
        *  return：string
        */
        public static string GetRequestApi(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }

        /*
          *  name:hcw
          *  time: 2020/12/17
          *  content：Post请求方式
          *  return：string
          */
        public static string PostRequestApi(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Post;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}