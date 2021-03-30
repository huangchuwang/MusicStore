using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class UserManagemenController : Controller
    {
        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：用户管理
        *  return：json
        */
        public ActionResult Index()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取用户数据
        *  return：string
        */
        [HttpGet]
        public string SelectList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetUsersList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：用户删除
        *  return：string
        */
        [HttpPost]
        public string Delete(string id)
        {

            string data = Invoke.PostRequestApi("http://localhost:8848/api/DelUsers?" + "id=" + id);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2021/1/7
        *  content：添加用户
        *  return：string
        */
        [HttpPost]
        public string CreateUsers(string roleid,string name,string account,string password,string email)
        {
 

            string data = Invoke.PostRequestApi("http://localhost:8848/api/AddAccount?"
            + "name=" + name
            + "&account=" + account
            + "&password=" + password
            + "&email=" + email
            + "&roleid=" + roleid
               );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：编辑用户
        *  return：string
        */
        [HttpPost]
        public string EditUsers(string id,string name, string account, string password, string email,string roleid)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/EditUsers?"
            + "name=" + name
            + "&id=" + id
            + "&account=" + account
            + "&password=" + password
            + "&email=" + email
            + "&roleid=" + roleid
            );
            return StringFormat.SF(data);
        }


          /*
          *  name:hcw
          *  time: 2020/12/17
          *  content：用户开关
          *  return：json
          */
        [HttpPost]
        public string UsersSwitch(string id,int sta)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/UserSwitch?" + "id="+id+"&sta="+sta);
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

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：模糊查询
        *  return：string
        */
        //[HttpPost]
        public string Search(string name)
        {
            string data = Invoke.GetRequestApi("http://localhost:8848/api/GetByUsers?" + "name=" + name);
            return StringFormat.SF(data);
        }
        

    }
}