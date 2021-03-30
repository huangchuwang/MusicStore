using NetMvcMusicStory.Admin.Unity.Invoke;
using NetMvcMusicStory.Admin.Unity.StrFormat;
using System.Web.Mvc;

namespace NetMvcMusicStory.Client.AdminControllers
{
    /*
     *  time: 2020/12/17
     *  订单管理
     */
    public class AdminOderItemsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：数据列表
        *  return：json
        */
        [HttpGet]
        public string GetList(int page = 1, int limit = 5)
        {
           string  data = Invoke.GetRequestApi("http://localhost:8085/api/GetOrderList?" + "page="+page+ "&limit=" + limit);
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


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content:补发订单
        *  return：json
        */
        [HttpPost]
        public string Create(int Quantity,string UserId,string ShoppingCartItemsId)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/AddOderItems?" 
                + "&quantity=" + Quantity
                + "&userid=" + UserId
                + "&shoppingcartitemsid=" + ShoppingCartItemsId
                );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content:编辑订单
        *  return：json
        */
        [HttpPost]
        public string EditOder(string Id, string Quantity)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/EditOderItems?"
            + "id=" + Id
            + "&quantity=" + Quantity
            );
            return StringFormat.SF(data);
        }


        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：订单搜索
        *  return：json
        */
        [HttpGet]
        public string Search(string id)
        {
            string data = Invoke.PostRequestApi("http://localhost:8085/api/Search?" + "oderid=" + id);
            return StringFormat.SF(data);
        }

    }
}