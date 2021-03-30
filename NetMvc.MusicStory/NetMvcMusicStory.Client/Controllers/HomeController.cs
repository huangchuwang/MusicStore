using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using NetMvcMusicStory.Client.Unity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.Controllers
{

    /*
    * 用户信息获取
    * 账号： string user = (string)Session["User"];
    * 头像：string user = (string)Session["Icon"];
    */

    /*
    *  name:hcw
    *  time: 2020/12/21
    *  content：客户端首页
    *  return：view
    */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string user = (string)Session["User"];
            return View();
        }


        public ActionResult error()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2021/1/4
        *  content：搜索页面
        *  return：view
        */
        public ActionResult Search(string name = null)
        {
            ViewBag.name = name;
            return View();
        }

        /*
        *  name:gjy
        *  time: 2021/1/7
        *  content：信息修改
        *  return：view
        */
        public ActionResult Edit_Information()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：模糊查询接口
        *  return：string
        */
        [HttpGet]
        public string SearchAlbum(string name)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/Search?" + "name=" + name);
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2020/12/29
        *  content：当打开登录弹窗时清除session
        *  return：view
        */
        public int Clear()
        {
            Session.Clear();
            return 0;
        }

        /*
        *  name:hcw
        *  time: 2020/12/29
        *  content：登录校验（为处理Session有效时间）
        *  return：view
        */
        [HttpPost]
        public string Check(string username, string password)
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/LoginCheck?" + "user=" + username + "&pass=" + password);
            data = StringFormat.SF(data);//清除多余反斜杠

            //反序列化拿到返回的数据
            Hashtable obj = JsonConvert.DeserializeObject<Hashtable>(data);
            string icon = (string)obj["icon"];//头像
            string name = (string)obj["name"];//用户昵称
            string user = (string)obj["user"];
            int code = int.Parse(obj["code"].ToString());
            string msg = (string)obj["msg"];

            if (code == 0)
            {
                Session["User"] = user;
                Session["Icon"] = icon;
                return FormatUtil.LoginResult(msg, code, icon, name);
            }
            return FormatUtil.LoginResult(msg, 1, icon, name);
        }

        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：用户注册
        *  return：string
        */
        [HttpPost]
        public string CreateUser(
                string name,
                string username,
                string pass,
                string email
            )
        {
            string data = Invoke.PostRequestApi("http://localhost:8848/api/AddUser?"
            + "name=" + name
            + "&account=" + username
            + "&pass=" + pass
            + "&email=" + email
            );
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/21
        *  content：专辑数据
        *  return：string
        */
        public string GetAlbumData(int page = 1, int limit = 8)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetAlbumList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：加入购物车
        *  return：string
        */
        [HttpPost]
        public string CreateShoppingcartItems(string albumid)
        {
            string user = (string)Session["User"];
            var data = Invoke.PostRequestApi("http://localhost:8886/api/AddShoppingCart?User=" + user + "&Albumid=" + albumid);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/10
        *  content：默认榜单
        *  return：string
        */
        [HttpPost]
        public string Default_list()
        {
            var data = Invoke.PostRequestApi("http://localhost:8899/api/GetMusicData?");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/10
        *  content：最新榜单
        *  return：string
        */
        [HttpGet]
        public string Newest_list()
        {
            var data = Invoke.GetRequestApi("http://localhost:8899/api/Newest_list");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：流量榜单
        *  return：string
        */
        [HttpGet]
        public string Flow_list()
        {
            var data = Invoke.GetRequestApi("http://localhost:8899/api/Flow_list");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/10
        *  content：热门榜单
        *  return：string
        */
        [HttpGet]
        public string Hot_list()
        {
            var data = Invoke.GetRequestApi("http://localhost:8899/api/Hot_list");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/10
        *  content：点赞榜单
        *  return：string
        */
        [HttpGet]
        public string Praise_list()
        {
            var data = Invoke.GetRequestApi("http://localhost:8899/api/Praise_list");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：好评榜单
        *  return：string
        */
        [HttpGet]
        public string Evaluate_list()
        {
            var data = Invoke.GetRequestApi("http://localhost:8899/api/Evaluate_list");
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/10
        *  content：流派榜单
        *  return：string
        */
        [HttpPost]
        public string Genre_list(string genrename)
        {
            var data = Invoke.PostRequestApi("http://localhost:8899/api/Genre_list?genrename=" + genrename);
            return StringFormat.SF(data);
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：个人信息
        *  return：string
        */
        [HttpGet]
        public string GetUser()
        {
            if (Session["User"] != null)
            {
                var data = Invoke.GetRequestApi("http://localhost:8899/api/GetUser?account=" + Session["User"]);
                return StringFormat.SF(data);
            }
            else
            {
                return "未登录";
            }
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：信息修改
        *  return：string
        */
        [HttpPost]
        public string Edit_User(string account, string name, string email, string icon)
        {
            if (Session["User"] != null)
            {
                var data = Invoke.PostRequestApi("http://localhost:8899/api/Edit_User?account=" + account + "&name=" + name + "&email=" + email + "&icon=" + icon);
                return StringFormat.SF(data);
            }
            else
            {
                return "未登录";
            }
        }

        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：图片上传
        *  return：json
        */
        public JsonResult UploadImgFile(HttpPostedFileBase imgFile)
        {
            if (imgFile.ContentLength == 0)
            {
                return Json(new
                {
                    upStatus = false,
                    upMsg = "请选择上传图片！"
                }, "text/html");
            }
            //生成文件名
            var timeStampString = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo);
            var suffix = imgFile.FileName.Substring(imgFile.FileName.IndexOf("."));//确定后缀名的起始位置
            //将系统生成的文件名替换为实际存储文件名
            if (!System.IO.Directory.Exists(Server.MapPath("~/Content/UploaFile/UserIcon")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/UploaFile/UserIcon"));//不存在就创建目录
            }
            var fileName = Path.Combine(Request.MapPath("~/Content/UploaFile/UserIcon"), Path.GetFileName(timeStampString + suffix));//此处可调优

            try
            {
                imgFile.SaveAs(fileName);//将文件存储至目标文件夹
                return Json(new
                {
                    imgUrlString = timeStampString + suffix,
                    upStatus = true,
                    upMsg = "图片上传成功！"
                });
            }
            catch
            {
                return Json(new
                {
                    upStatus = false,
                    upMsg = "图片上传失败！"
                });
            }
        }
    }
}

