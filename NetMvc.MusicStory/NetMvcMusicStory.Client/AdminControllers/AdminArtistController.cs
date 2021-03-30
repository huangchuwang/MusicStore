using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{       /*
        *  time: 2020/12/17
        *  content：流派管理模块
        */
    public class AdminArtistController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：数据列表
        *  return：json
        */
        [HttpGet]
        public string GetList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/getArtistList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：删除数据
        *  return：json
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/DelArtist?" + "id=" + id);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：添加数据
        *  return：json
        */
        [HttpPost]
        public string CreateArtist(string name, string description)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/AddArtist?" + "name=" + name + "&description=" + description);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：编辑数据
        *  return：json
        */
        [HttpPost]
        public string EditArtist(string id, string name, string description)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/EditArtist?"
            + "id=" + id
            + "&name=" + name
            + "&description=" + description
            );
            return StringFormat.SF(data);
        }


        /// <summary>
        /// 模糊查询接口
        /// </summary>
        /// <returns>json</returns>

        public string GetOneArtist(string name)
        {
            return "";
        }



    }
}