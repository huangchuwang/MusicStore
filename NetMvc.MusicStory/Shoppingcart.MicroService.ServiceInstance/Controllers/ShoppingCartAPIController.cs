using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Entity.EntityClass;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Shoppingcart.MicroService.ServiceInstance.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Shoppingcart.MicroService.ServiceInstance.Controllers
{
    public class ShoppingCartAPIController : ApiController
    {
        private readonly IRepository<ShoppingCartItems> _repositorysc;//购物车商品
        private readonly MusicStoreDbContext _context = new MusicStoreDbContext();

        public ShoppingCartAPIController()
        {
            _repositorysc = new Repository<ShoppingCartItems>(_context);
        }

        /// <summary>
        /// Api获取购物车数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetShoppingCart")]
        public string GetShoppingCart(string User)
        {
            ArrayList allData = new ArrayList();
            var discountAlbum = _repositorysc.GetAllRalevance<Discount>().Where(x => x.Statu == 1).ToList();
            Guid user = _repositorysc.GetAllRalevance<User>().Where(x => x.Account == User).Select(y => y.Id).FirstOrDefault();
            Guid Cartid = _repositorysc.GetAllRalevance<ShoppingCart>().Where(x => x.UserId == user).Select(y => y.Id).FirstOrDefault();
            string discountAlbumId = "";
            foreach (var item in discountAlbum)
            {
                TimeSpan ts = DateTime.Now.Subtract(item.EndTime);
                if (ts.TotalDays < 0)
                {
                    var discountData = _repositorysc
                        .GetAll()
                        .Where(x => x.ShoppingCartId == Cartid)
                        .Where(x => x.Statu == 0).Where(x => x.Album.Id == item.AlbumId)
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            AlbumId = x.Album.Id,
                            AlbumName = x.Album.Name,//关联查出他对应的Album的名称
                            Genre = x.Album.Genre.Name,
                            x.Price,
                            x.Quantity,
                            x.SubTotalPrice,
                            AlbumDescription = x.Album.Description,
                            AlbumUrl = x.Album.UrlString,
                            AlbumDate = x.Album.OperationDate,
                            UserName = x.ShoppingCart.User.Name,
                            DiscountPrice = x.Price * item.AlbumDiscount
                        })
                        .ToList();
                    discountAlbumId += item.AlbumId + ",";
                    foreach (var data in discountData)
                    {
                        allData.Add(data);
                    }
                }
            }
            var noDiscountData = _repositorysc
            .GetAll()
            .Where(x => x.ShoppingCartId == Cartid)
            .Where(x => x.Statu == 0)
            .Select(x => new
            {
                x.Id,
                x.Name,
                AlbumId = x.Album.Id,
                AlbumName = x.Album.Name,//关联查出他对应的Album的名称
                Genre = x.Album.Genre.Name,
                x.Price,
                x.Quantity,
                x.SubTotalPrice,
                AlbumDescription = x.Album.Description,
                AlbumUrl = x.Album.UrlString,
                AlbumDate = x.Album.OperationDate,
                UserName = x.ShoppingCart.User.Name,
                DiscountPrice = x.Price
            })
            .ToList();
            foreach (var item in noDiscountData)
            {
                if (!discountAlbumId.Contains(item.AlbumId.ToString()))
                {
                    allData.Add(item);
                }
            }
            if (allData.Count == 0)
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            else
                return FormatUtil.JsonFormat(allData.Count, "查询成功", 0, allData);
        }


        [HttpPost]
        [Route("api/AddShoppingCart")]
        public string AddShoppingCart(string User, string AlbumId)
        {
            Guid albumId = Guid.Parse(AlbumId);
            Guid user = _repositorysc.GetAllRalevance<User>().Where(x => x.Account == User).Select(y => y.Id).FirstOrDefault();
            Guid Cartid = _repositorysc.GetAllRalevance<ShoppingCart>().Where(x => x.UserId == user).Select(y => y.Id).FirstOrDefault();
            Album album = _repositorysc.GetAllRalevance<Album>().Where(x => x.Id == albumId).FirstOrDefault();

            var temp = _repositorysc.GetAll().Where(x => x.ShoppingCartId == Cartid).Where(y => y.AlbumId == album.Id).Where(z => z.Statu == 0).FirstOrDefault();
            if (temp != null)
            {
                temp.Quantity++;
                temp.SubTotalPrice = temp.Quantity * temp.Price;
                int statu = _repositorysc.EditAndSave(temp);
                if (statu != 1)
                    return FormatUtil.JsonFormat(0, "添加失败", 0, "");
                else
                    return FormatUtil.JsonFormat(1, "添加成功", 0, "");
            }
            else
            {
                ShoppingCartItems data = new ShoppingCartItems
                {
                    Id = Guid.NewGuid(),
                    AlbumId = album.Id,
                    Name = album.Name,
                    Price = album.Price,
                    Quantity = 1,
                    ShoppingCartId = Cartid,
                    SubTotalPrice = album.Price * 1,
                    Statu = 0
                };
                int statu = _repositorysc.AddAndSave(data);
                if (statu != 1)
                    return FormatUtil.JsonFormat(0, "添加失败", 0, "");
                else
                    return FormatUtil.JsonFormat(1, "添加成功", 0, "");
            }
        }


        [HttpPost]
        [Route("api/EditShoppingCartItems")]
        public string Edit(string ShoppingcartItemId, int Quantity, int SubTotalPrice)
        {
            decimal subTotalPrice = Convert.ToInt64(SubTotalPrice);
            Guid shoppingcartItemId = Guid.Parse(ShoppingcartItemId);
            ShoppingCartItems shoppingCartItem = _repositorysc.GetSingle(shoppingcartItemId);
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Quantity = Quantity;
                shoppingCartItem.SubTotalPrice = subTotalPrice;
                int statu = _repositorysc.EditAndSave(shoppingCartItem);
                if (statu != 1)
                    return FormatUtil.JsonFormat(0, "保存失败", 0, "");
                else
                    return FormatUtil.JsonFormat(1, "保存成功", 0, "");
            }
            else
                return FormatUtil.JsonFormat(0, "数据不存在", 0, "");
        }



        [HttpGet]
        [Route("api/DeleteShoppingCartItems")]
        public string Delete(string id)
        {
            ShoppingCartItems shoppingCartItems = _repositorysc.GetSingle(Guid.Parse(id));
            shoppingCartItems.Statu = 1;
            int statu = _repositorysc.EditAndSave(shoppingCartItems);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "");
        }
    }
}
