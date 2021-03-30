using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{



    public class AdminMusicController : Controller
    {
        readonly FileUploadController file;
        public AdminMusicController(FileUploadController file)
        {
            this.file = file;
        }
        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：音乐管理页面
        *  return：string
        */
        public ActionResult Index()
        {
            return View();
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取音乐数据
        *  return：string
        */
        [HttpGet]
        public string SelectList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetMusicList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：音乐删除
        *  return：string
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/DelMusic?" + "id=" + id);
            //string url = Server.MapPath(genre.MusicUrl);//获取绝对路径
            //file.DeleteFiles(url);//调用删除文件方法
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：添加音乐数据
        *  return：string
        */
        [HttpPost]
        public string CreateMusic(string albumid, string name,string musicurl,string description)
        {
            
              string data = Invoke.PostRequestApi("http://localhost:8085/api/AddMusic?" 
                + "albumid=" + albumid
                + "&name=" + name
                + "&musicurl=" + musicurl
                + "&description=" + description
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
        public string EditMusic(string id, string albumid, string name, string musicurl, string description)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/EditMusic?"
            + "id=" + id
            + "&albumid=" + albumid
            + "&name=" + name
            + "&musicurl=" + musicurl
            + "&description=" + description
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
        public string GetOneMusic(string name)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetByMusic?" + "name=" + name);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取某个专辑下的所有音乐
        *  return：json
        */
        //[HttpPost]
        //public string GetMusic(string AlbumId)
        //{
        //    string data = Invoke.PostRequestApi("http://localhost:8085/api/GetAlbumByMusicList?" + "albumid=" + AlbumId);
        //    return StringFormat.SF(data);
        // }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：专辑下拉菜单
        *  return：json
        */
        [HttpPost]
        public string SelectMenu()
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/GetAlbumMuen");
            return StringFormat.SF(data);
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：生成歌词文件
        *  return：json
        */
        [HttpPost]
        public string CreateLrcFile(string Id, string Lrc, string LrcContent)
        {
            string success = null;//返回结果
            string filename = null;//相对路径
            string path = null;//绝对路径
            {   //获取资源地址相对路径
                filename = "../Content/UploaFile/Lrc/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".lrc";
                //获取绝对路径   
                path = Server.MapPath(filename);
                //判断歌词地址是否为空，为空则创建歌词文件
                if (Lrc == "")
                {
                    file.CreateLrc(path, LrcContent); //生成歌词文件
                                                      //地址入库
                    success = Invoke.PostRequestApi("http://localhost:8085/api/AddLrc?" + "id=" + Id + "&url=" + filename);
                    return StringFormat.SF(success);
                }
                else
                {
                    string statu = file.EditLrc(path, LrcContent);//文件编辑
                    return statu;
                }

            }
        }


        ///*
        //*  name:hcw
        //*  time: 2020/12/20
        //*  content：IO读取歌词文件数据
        //*  return：json
        //*/
        //[HttpPost]
        //public string GetLrcData(string Id, string lrc)
        //{
        //    FileUploadController ob = new FileUploadController();
        //    ob.CreateLrc(lrc);//生成或写入歌词文件
        //    string data = Invoke.PostRequestApi("http://localhost:8085/api/GetAlbumByMusicList");
        //    return StringFormat.SF(data);
        //}

    }
}