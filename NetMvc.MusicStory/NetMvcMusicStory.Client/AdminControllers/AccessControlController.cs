using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using NetMvcMusicStory.Admin.Unity.Invoke;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class AccessControlController : Controller
    {
        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：数据列表
        *  return：json
        */
        public ActionResult Index()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取规则数据
        *  return：string
        */
        [HttpGet]
        public string SelectList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetUserRulesList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：角色下拉菜单
        *  return：json
        */
        [HttpPost]
        public string SelectMenu()
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/GetUserRolesMuen");
            return StringFormat.SF(data);
        }
    }
}