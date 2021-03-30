using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    public class ShoppingCartManageController : Controller
    {
        // GET: ShoppingCartManage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(string id)
        {
            ViewBag.id= id;
            return View();
        }


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：用户商品列表
        *  return：json
        */
        [HttpGet]
        public string GetItemDataList(string id)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetShoppingCartItemsData?id=" + id);
            return StringFormat.SF(data);
        }
        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：获取购物车数据
        *  return：json
        */
        [HttpGet]
        public string GetDataList(int page = 1, int limit = 5)
        {
            string data = Invoke.GetRequestApi("http://localhost:8085/api/GetShoppingCartList?" + "page=" + page + "&limit=" + limit);
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：删除数据
        *  return：json
        */
        [HttpPost]
        public string Delete(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/DelShoppingCartItems?" + "id=" + id);
            return StringFormat.SF(data);
        }


    }
}