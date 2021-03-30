using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.Controllers
{
    public class ShoppingCartController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public string GetAllShoppingCart()
        {
            string user = (string)Session["User"];
            var data = Invoke.GetRequestApi("http://localhost:8886/api/GetShoppingCart?User=" + user);
            return StringFormat.SF(data);
        }

        [HttpPost]
        public string EditGetAllShoppingCart(string ShoppingcartItemId, int Quantity, int SubTotalPrice)
        {
            var data = Invoke.PostRequestApi("http://localhost:8886/api/EditShoppingCartItems?ShoppingcartItemId=" + ShoppingcartItemId + "&Quantity=" + Quantity + "&SubTotalPrice=" + SubTotalPrice);
            return StringFormat.SF(data);
        }

        public string DeleteSingleShoppingCartItems(string id)
        {
            var data = Invoke.GetRequestApi("http://localhost:8886/api/DeleteShoppingCartItems?id=" + id);
            return StringFormat.SF(data);
        }

        public ActionResult CreateShoppingcartItems(string Albumid)
        {
            string user = (string)Session["User"];
            if (user == "" || user == null)
            {
                return Content("<script>alert('请先登录再进行添加操作！');location.href='/PlayMusic/Index?albumid=" + Albumid + "'</script>");
            }
            var data = Invoke.PostRequestApi("http://localhost:8886/api/AddShoppingCart?User=" + user + "&Albumid=" + Albumid);
            return Content("<script>alert('添加成功');location.href='/ShoppingCart/Index'</script>");
        }
    }
}