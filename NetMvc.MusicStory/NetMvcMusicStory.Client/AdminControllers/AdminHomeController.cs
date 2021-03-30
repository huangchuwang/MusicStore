using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using NetMvcMusicStory.Client.Unity;
using RestSharp;
using System.Web.Mvc;
namespace NetMvcMusicStory.Client.AdminControllers
{
    public class AdminHomeController: Controller
    {      
        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View();
        }

        /*
        *  name:hcw
        *  time: 2021/1/8
        *  content：获取菜单
        *  return：string
        */
        [HttpGet]
        public string getMenu()
        {
            string account=(string)Session["AdminUser"];
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetRulesMenu?Acc="+ account);
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2020/12/29
        *  content：清除session
        *  return：view
        */
        public string Clear()
        {
            Session.Clear();
            return FormatUtil.LoginResult("清除成功", 0, null,"");
        }
    }
}