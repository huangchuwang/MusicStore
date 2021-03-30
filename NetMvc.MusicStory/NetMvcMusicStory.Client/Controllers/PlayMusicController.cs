using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.IO;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.Controllers
{
    public class PlayMusicController : Controller
    {
        public ActionResult Index(string albumid)
        {
            ViewBag.id = albumid;
            return View();
        }

        public ActionResult Comment()
        {
            return View();
        }

        /*
        *  name:lzc
        *  time: 2020/12/25
        *  content：获取音乐数据
        *  return：string
        */

        [HttpGet]
        public string GetAllPlayMusic(string albumid, int page = 1, int limit = 5)
        {
            Guid id = new Guid(albumid);
            var data = Invoke.GetRequestApi("http://localhost:8899/api/GetMusicData?" + "id=" + id + "&page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2020/12/30
        *  content：获取评论数据
        *  return：string
        */

        [HttpGet]
        public string GetAlbumComment(string albumid)
        {
            Guid id = new Guid(albumid);
            var data = Invoke.GetRequestApi("http://localhost:8899/api/GetCommentData?" + "id=" + id);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/01/04
        *  content：发表评论
        *  return：string
        */

        [HttpPost]
        public string CreateComment(string albumid, string account, string content, string star)
        {
            Guid aid = new Guid(albumid);
            double star2 = Convert.ToDouble(star);
            var data = Invoke.PostRequestApi("http://localhost:8899/api/GetCommentData?" + "albumid=" + aid + "&account=" + account + "&content=" + content + "&star=" + star2);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：点赞专辑
        *  return：string
        */

        [HttpPost]
        public string HandlePraise(string albumid, string account)
        {
            Guid aid = new Guid(albumid);
            var data = Invoke.PostRequestApi("http://localhost:8899/api/HandlePraise?" + "albumid=" + aid + "&account=" + account);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：点赞数量
        *  return：int
        */
        [HttpPost]
        public int PraiseCount(string albumid)
        {
            Guid aid = new Guid(albumid);
            var data = Invoke.PostRequestApi("http://localhost:8899/api/PraiseCount?" + "albumid=" + aid);
            return Convert.ToInt32(data);
        }

        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：检索点赞
        *  return：bool
        */
        [HttpPost]
        public bool Inspect_Praise(string albumid, string account)
        {
            Guid aid = new Guid(albumid);
            var data = Invoke.PostRequestApi("http://localhost:8899/api/Inspect_Praise?" + "albumid=" + aid + "&account=" + account);
            return Convert.ToBoolean(data);
        }

        /*
        *  name:lzc
        *  time: 2020/12/20
        *  content：获取音乐歌词
        *  return：string
        */
        [HttpGet]
        public string Getgc(string file)
        {
            string route = Server.MapPath("..");
            try
            {
                using (StreamReader sr = new StreamReader(route + file))
                {
                    string data = "";
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    int i = 0;
                    string a = "";
                    while ((a += sr.ReadLine()) != "")
                    {
                        data += a + "\n";
                        i++;
                        a = "";
                    }
                    return data;
                }
            }
            catch
            {
                return "暂无歌词";
            }
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：增加专辑播放量
        *  return：string
        */
        [HttpPost]
        public string PlayNumberAdd(string albumid)
        {
            Guid id = new Guid(albumid);
            var data = Invoke.PostRequestApi("http://localhost:8899/api/PlayNumberAdd?albumid=" + id);
            return StringFormat.SF(data);
        }
    }
}