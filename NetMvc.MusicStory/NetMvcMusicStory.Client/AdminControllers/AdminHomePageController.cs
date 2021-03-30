using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class AdminHomePageController : Controller
    {


        public ActionResult HomePage()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/19
        *  content：图形化统计数据
        *  return：string
        */
        [HttpPost]
        public string GenreByCountList()
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/GenreByCountList");
            return StringFormat.SF(data);
        }

    }
}