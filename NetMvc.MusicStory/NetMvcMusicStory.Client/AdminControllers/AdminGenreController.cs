using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    /*
     *  time: 2020/12/17
     *  content：歌手管理模块
     */
    public class AdminGenreController : Controller
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
            //这里要进行负载均衡
            //调用API
           string  data = Invoke.GetRequestApi("http://localhost:8085/api/GetGenreList?" + "page="+page+ "&limit=" + limit);
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
            string data = Invoke.PostRequestApi("http://localhost:8085/api/DelGenre?" + "id=" + id);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：添加数据
        *  return：json
        */
        [HttpPost]
        public string CreateGenre(string name, string description)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/AddGenre?" + "name=" + name + "&description=" + description);
            return StringFormat.SF(data);
        }


        /// <summary>
        ///编辑数据   
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns>json</returns>
        [HttpPost]
        public string EditGenre(string id, string name, string description)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/EditGenre?"
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

        public string GetOneGenre(string name)
        {
            return "";
        }




    }
}