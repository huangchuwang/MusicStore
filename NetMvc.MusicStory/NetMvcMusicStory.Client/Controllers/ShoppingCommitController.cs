using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.Controllers
{
    public class ShoppingCommitController : Controller
    {
        // GET: ShoppingCommit
        public ActionResult Index()
        {
            return View();

        }
        public string GetAllOrderData()
        {
            string user = (string)Session["User"];
            var data = Invoke.GetRequestApi("http://localhost:8886/api/GetOrderList?Account=" + user);
            return StringFormat.SF(data);
        }
        [HttpPost]
        public ActionResult OrderSubmit(string data)
        {
            ViewBag.Data = CartData(data);
            return View();
        }

        [HttpPost]
        public string CartData(string data)
        {
            var newData = Invoke.PostRequestApi("http://localhost:5551/api/CartData?data=" + data);
            return StringFormat.SF(newData);
        }


        //[HttpPost]
        //[Route("api/CartData")]
        //public string CartData(List<string> data)
        //{
        //    decimal count = 0;
        //    List<OrderItems> list = new List<OrderItems>();
        //    foreach (var item in data)
        //    {
        //        OrderItems orderitems = _repository.GetSingle(Guid.Parse(item));
        //        count += orderitems.SubPrice;
        //        list.Add(orderitems);
        //    }
        //    Hashtable has = new Hashtable();
        //    has.Add("Count", count);
        //    has.Add("data", list);
        //    return FormatUtil.JsonFormat(0, "", 0, has);
        //}



        public string GetAllOrder()
        {
            string user = (string)Session["User"];
            var data = Invoke.GetRequestApi("http://localhost:5551/api/GetAllOrderList?Account=" + user);
            return StringFormat.SF(data);
        }
        [HttpPost]
        public string AddOrders(string id)
        {
            string user = (string)Session["User"];
            var data = Invoke.PostRequestApi("http://localhost:5551/api/AddOrder?Account=" + user + "&Shoppingcartitemid=" + id);
            return StringFormat.SF(data);
        }
        public string DeleteSingleOrder(string id)
        {
            var data = Invoke.GetRequestApi("http://localhost:5551/api/DeleteOrder?id=" + id);
            return StringFormat.SF(data);
        }
    }
}