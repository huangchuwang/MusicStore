using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class RoleManagemenController : Controller
    {
        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：角色管理
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
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetRolesList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：角色删除
        *  return：string
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/DelRoles?" + "id=" + id);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2021/1/7
        *  content：添加角色
        *  return：string
        */
        [HttpPost]
        public string CreateRule(string title)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/AddRoles?"
              + "title=" + title
               );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：编辑角色
        *  return：string
        */
        [HttpPost]
        public string EditRoles(string id, string title)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/EditRoles?"
            + "id=" + id
            + "&title=" + title
            );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：角色开关
        *  return：json
        */
        [HttpPost]
        public string RoleSwitch(string id,int sta)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/RoleSwitch?" + "id="+id+"&sta="+sta);
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
            string data = Invoke.PostRequestApi("http://localhost:8848/api/GetRulesMuen");
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：模糊查询接口
        *  return：json
        */
        [HttpGet]
        public string GetOneRole(string title)
        {
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetByRoles?" + "title=" + title);
            return StringFormat.SF(data);
        }

    }
}