using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using NetMvcMusicStory.Client.Unity;
using Newtonsoft.Json;
using System.Collections;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class AdminLoginController : Controller
    {
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }


        /*
        *  name:hcw
        *  time: 2020/12/29
        *  content：管理员登录校验（为处理Session有效时间）
        *  return：view
        */
        [HttpPost]
        public string AdminLoginCheck(string users, string pass)
        {
                string data = Invoke.PostRequestApi("http://localhost:8848/api/AdminLoginCheck?" + "user=" + users + "&pass=" + pass);
                data = StringFormat.SF(data);//清除多余反斜杠

                //反序列化拿到返回的数据
                Hashtable obj = JsonConvert.DeserializeObject<Hashtable>(data);
                string icon = (string)obj["icon"];//头像
                string name = (string)obj["name"];//用户昵称
                string AdminUser = (string)obj["user"];
                int code = int.Parse(obj["code"].ToString());
                string msg = (string)obj["msg"];

                if (code == 0)
                {
                    Session["AdminUser"] = AdminUser;
                    Session["Icon"] = icon;
                    return FormatUtil.LoginResult(msg, code, icon, name);
                }
                    return FormatUtil.LoginResult(msg, 1, icon, name);
        }

    }
}