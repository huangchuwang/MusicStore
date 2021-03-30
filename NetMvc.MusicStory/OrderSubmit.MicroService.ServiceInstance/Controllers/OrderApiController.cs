using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using PuApi.MicroService.ServiceInstance.Unity;
using Shoppingcart.MicroService.ServiceInstance.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderSubmit.MicroService.ServiceInstance.Controllers
{
    public class OrderApiController : ApiController
    {
        private readonly IRepository<OrderItems> _repository;
        private readonly MusicStoreDbContext _context = new MusicStoreDbContext();
        public OrderApiController()
        {
            _repository = new Repository<OrderItems>(_context);
        }
        /// <summary>
        /// Api获取订单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetAllOrderList")]
        public string GetAllOrderList(string Account)
        {
            Guid user = _repository.GetAllRalevance<User>().Where(x => x.Account == Account).Select(y => y.Id).FirstOrDefault();
            var data = _repository
                .GetAll()
                .Where(x => x.UserId == user)
                .Where(x => x.Is_delete == 0)
                .Select(x => new
                {
                    x.Id,
                    x.ShoppingCartItems.Album.Name,//关联查出他对应的Album的名称
                    AlbumDescription = x.ShoppingCartItems.Album.Description,
                    AlbumUrl = x.ShoppingCartItems.Album.UrlString,
                    AlbumDate = x.ShoppingCartItems.Album.OperationDate,
                    x.OperationDate,
                    x.ShoppingCartItems.AlbumId,
                    x.ShoppingCartItems.Price,
                    x.ShoppingCartItems.Quantity,
                    SubTotalPrice = x.ShoppingCartItems.Quantity * x.ShoppingCartItems.Price,
                    AmountActuallyPaid = x.ShoppingCartItems.SubTotalPrice //实付金额
                })
                .ToList();

            if (data.Count() == 0)
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            else
                return FormatUtil.JsonFormat(data.Count, "查询成功", 0, data);
        }
        [HttpPost]
        [Route("api/AddOrder")]
        public string AddOrder(string Account, string Shoppingcartitemid)
        {
            string orderNumber = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff").Replace("-", "").Replace(":", "").Replace(" ", "");
            Guid shoppingcartitemid = Guid.Parse(Shoppingcartitemid);
            Guid user = _repository.GetAllRalevance<User>().Where(x => x.Account == Account).Select(y => y.Id).FirstOrDefault();
            ShoppingCartItems shoppingcartitems = _repository.GetAllRalevance<ShoppingCartItems>().Where(x => x.Id == shoppingcartitemid).FirstOrDefault();
            OrderItems obj = new OrderItems
            {
                Id = Guid.NewGuid(),
                Name = shoppingcartitems.Name,
                OrderNember = orderNumber,
                OrderTime = DateTime.Now,
                OperationDate = DateTime.Now,
                Is_delete = 0,
                UserId = user,
                PayMethod = null,
                ShoppingCartItemsId = shoppingcartitems.Id,
                Quantity = shoppingcartitems.Quantity,
                Price = shoppingcartitems.Price,
                SubPrice = shoppingcartitems.SubTotalPrice
            };
            int statu = _repository.AddAndSave(obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "添加失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "");
        }
        [HttpGet]
        [Route("api/DeleteOrder")]
        public string Delete(string id)
        {
            OrderItems orderitems = _repository.GetSingle(Guid.Parse(id));
            if (orderitems.Is_delete == 0)
            {
                orderitems.Is_delete = 1;
                _repository.EditAndSave(orderitems);
                return FormatUtil.JsonFormat(1, "删除成功", 0, "");
            }
            else
                return FormatUtil.JsonFormat(0, "删除失败", 0, "");
        }


        public static string SF(string data)
        {
            object json = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(data)));
            return System.Convert.ToString(json);
        }

        [HttpPost]
        [Route("api/CartData")]
        public string CartData(string data)
        {
            string newData = SF(data);
            List<ID> rbList = JsonConvert.DeserializeObject<List<ID>>(newData);
            ArrayList list = new ArrayList();
            foreach (var item in rbList)
            {
                Guid guid = Guid.Parse(item.Id);
                var shoppingCartItems = _repository.GetAllRalevance<ShoppingCartItems>().Where(x => x.Id == guid).Select(
                    x => new {
                        x.Id,
                        x.Name,
                        AlbumName = x.Album.Name,//关联查出他对应的Album的名称
                        x.Price,
                        x.Quantity,
                        x.SubTotalPrice,
                        AlbumDescription = x.Album.Description,
                        AlbumUrl = x.Album.UrlString,
                        AlbumDate = x.Album.OperationDate,
                        UserName = x.ShoppingCart.User.Name,
                        UserEmail = x.ShoppingCart.User.Email,
                        AmountActuallyPaid = (x.SubTotalPrice / x.Quantity)
                    });
                list.Add(shoppingCartItems);
            }
            Hashtable has = new Hashtable();
            has.Add("data", list);
            return FormatUtil.JsonFormat(0, "", 0, has);
        }
    }
}

