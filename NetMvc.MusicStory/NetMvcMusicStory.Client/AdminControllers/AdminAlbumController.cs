using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class AdminAlbumController : Controller
    {

         public ActionResult Index()
        {
            return View();
        }

        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：数据列表接口
        *  return：string
        */
        [HttpGet]
        public string SelectList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetAlbumList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：删除数据
        *  return：string
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/DelAlbum?" + "id=" + id );
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：添加数据
        *  return：string
        */
        [HttpPost]
        public string CreateAlbum(
            string name,
            string genre, 
            string artist,  
            decimal price,
            DateTime operationdate,
            string description)
        {

            string data = Invoke.PostRequestApi("http://localhost:8085/api/AddAlbum?"
            + "name=" + name
            + "&genreid=" + genre
            + "&artistid=" + artist
            + "&price=" + price
            + "&operationdate=" + operationdate
            + "&description=" + description
            );
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：编辑数据
        *  return：string
        */
        [HttpPost]
        public string EditAlbum(
            string id,
            string genre,
            string artist,
            string Name,
            decimal Price,
            DateTime OperationDate,
            string Description
            )
        {

            string data = Invoke.PostRequestApi("http://localhost:8085/api/EditAlbum?"
            + "id=" + id
            + "&name=" + Name
            + "&genreid=" + genre
            + "&artistid=" + artist
            + "&price=" + Price
            + "&description=" + Description
            + "&operationdate=" + OperationDate

            );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：添加或修改图片
        *  return：string
        */
        [HttpPost]
        public string EditAImg(string id, string urlstring)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/AddOrEditImg?" + "id=" + id+"&url="+urlstring);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：模糊查询接口
        *  return：string
        */
        [HttpGet]
        public string GetAlbum(string name)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/Search?" + "name=" + name);
            return StringFormat.SF(data);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：歌手数据下拉菜单接口
        *  return：string
        */
        [HttpPost]
        public string ArtistSelectMenu()
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/GetArtistMuen");
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：流派数据下拉菜单接口
        *  return：string
        */
        [HttpPost]
        public string GenreSelectMenu()
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/GetGenreMuen");
            return StringFormat.SF(data);
        }

    }
}