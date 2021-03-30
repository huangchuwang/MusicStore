using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Linq;
using System.Web.Http;

namespace PuApi.MicroService.ServiceInstance.Controllers
{
    public class OrderManageController : ApiController
    {
        private readonly IRepository<OrderItems> _orderitems;
        private readonly IRepository<User> _user;
        private readonly IRepository<ShoppingCartItems> _s_citem;
        MusicStoreDbContext _context;
        public OrderManageController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _orderitems = new Repository<OrderItems>(_context);
            _user = new Repository<User>(_context);
            _s_citem = new Repository<ShoppingCartItems>(_context);
        }


        /*
         *  name:hcw
         *  time: 2021/1/12
         *  content：订单列表
         *  return：json
         */
        [HttpGet]
        [Route("api/GetOrderList")]
        public string GetOrderList(int page = 1, int limit = 5)
        {
            int count = _orderitems.GetAll().Count();//总条数        
            var data = _orderitems.GetAll().Select(x => new
            {
                x.Id,
                x.OrderTime,
                x.OperationDate,
                x.Is_delete,
                x.PayMethod,
                Business = x.ShoppingCartItems.Name,
                x.Quantity,
                x.Price,
                x.SubPrice,
                x.ShoppingCartItemsId,
                x.UserId
                //UserName = _user.GetSingle(x.UserId).Name//拿到用户的id
            })
                .OrderBy(x => x.Id)
                .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
                .Take(limit) //返回指定条数
                .ToList();
            if (count == 0)
            {
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            }
                return FormatUtil.JsonFormat(count, "查询成功", 0, data);
        }


        /*
        *  name:hcw
        *  time: 2021/1/12
        *  content：删除订单
        *  return：statu
        */
        [Route("api/DelOderItems")]
        public string DelShoppingCartItems(Guid id)
        {
            OrderItems oder = _orderitems.GetSingle(id);
            int statu = _orderitems.DeleteAndSave(oder);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/12
        *  content:订单查询
        *  return：json
        */
        //[Route("api/Search")]
        //[HttpGet]
        //public string Search(string orderid)
        //{
        //    var obj = _orderitems.GetAll()              
        //       .Select(x => new
        //      {
        //        x.Id,
        //        x.OrderTime,
        //        x.OperationDate,
        //        x.Is_delete,
        //        x.PayMethod,
        //        Business = x.ShoppingCartItems.Name,
        //        ShoppingCartItemsId = x.ShoppingCartItems.Id,//订单商品Id
        //        x.Quantity,
        //        x.Price,
        //        x.SubPrice,
        //        UserName= _user.GetSingle(x.UserId),//所属用户
        //    })
        //      .Where(x => x.OrderNumber.Contains(orderid))
        //    .OrderBy(x => x.Id)
        //    .ToList();
        //    int count = obj.Count();//统计数量
        //    if (obj.Count() == 0)
        //    {
        //        return FormatUtil.JsonFormat(count, "没有数据", 1, "");
        //    }
        //    return FormatUtil.JsonFormat(count, "成功", 0, obj);
        //}


        /*
         *  name:hcw
         *  time: 2021/1/12
         *  content：补发订单
         *  return：json
         */
        [Route("api/AddOderItems")]
        [HttpPost]
        public string AddOderItems(
               int quantity,
               Guid userid,
               Guid shoppingcartitemsid
            )
        {
            OrderItems obj = new OrderItems()
            {
                Is_delete = 0,
                Quantity = quantity,
                PayMethod="补发",
                UserId = userid,
                ShoppingCartItemsId= shoppingcartitemsid,
                OperationDate =DateTime.Now,
                OrderTime=DateTime.Now
            };
            int statu = _orderitems.AddAndSave(obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "添加失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "");
        }



        /*
         *  name:hcw
         *  time: 2021/1/12
         *  content：修改订单
         *  return：json
         */
        [Route("api/EditOderItems")]
        [HttpPost]
        public string EditOderItems(
                Guid id,
               int quantity
            )
        {
            OrderItems obj= _orderitems.GetSingle(id);
            obj.Quantity = quantity;

            obj.OperationDate = DateTime.Now;
            int statu = _orderitems.EditAndSave(obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "添加失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "");
        }
    }
}
