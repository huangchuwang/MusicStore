using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class RuleManagementController : Controller
    {
        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：规则管理
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
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetRulesList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：删除规则
        *  return：string
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/DelRules?" + "id=" + id);
            //string url = Server.MapPath(genre.MusicUrl);//获取绝对路径
            //file.DeleteFiles(url);//调用删除文件方法
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：新增规则
        *  return：string
        */
        [HttpPost]
        public string CreateRule(string title, string menuurl, string roleid)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/AddRules?"
              +"title=" + title
              +"&menuurl=" + menuurl
              +"&roleid="+ roleid
               );
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：编辑数据
        *  return：string
        */
        [HttpPost]
        public string EditRule(string id, string title, string menuurl, string roleid)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/EditRules?"
            + "id=" + id
            + "&title=" + title
            + "&menuurl=" + menuurl
            + "&roleid=" + roleid
            );
            return StringFormat.SF(data);
        }


        /*
          *  name:hcw
          *  time: 2020/12/17
          *  content：模糊查询接口
          *  return：json
          */
        [HttpGet]
        public string GetOneRule(string title)
        {
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetByRules?" + "title=" + title);
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：角色开关
        *  return：json
        */
        [HttpPost]
        public string RuleSwitch(string id, int sta)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/RuleSwitch?" + "id=" + id + "&sta=" + sta);
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
            string data = Invoke.PostRequestApi("http://localhost:8848/api/GetRolesMuen");
            return StringFormat.SF(data);
        }
    }
}