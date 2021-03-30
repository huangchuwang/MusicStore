using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Linq;
using System.Web.Http;

namespace PuApi.MicroService.ServiceInstance.Controllers
{
    public class ShoppingCartManageController : ApiController
    {
        private readonly IRepository<ShoppingCartItems> _scit;
        private readonly IRepository<ShoppingCart> _sc;
        MusicStoreDbContext _context;
        public ShoppingCartManageController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }

        _sc = new Repository<ShoppingCart>(_context);
        _scit = new Repository<ShoppingCartItems>(_context);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：Api获取专辑数据列表
        *  return：json
        */
        [HttpGet]
        [Route("api/GetShoppingCartList")]
        public string GetShoppingCartList(int page = 1, int limit = 5)
        {
            int count = _sc.GetAll().Count();//总条数        
            var data = _sc.GetAll().Select(x => new
            {
                x.Id,
                UserName =x.User.Name,
                x.Count,
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
        *  time: 2020/12/17
        *  content：获取购物车中的商品数据
        *  return：json
        */
        [HttpGet]
        [Route("api/GetShoppingCartItemsData")]
        public string GetShoppingCartItemsData(Guid id)
        {
            var data = _scit.GetAll()
                .Where(x=>x.ShoppingCartId==id)
                .Select(x => new { 
                x.Id, 
                x.Name,
               x. Quantity,
               x. Price,
               x. SubTotalPrice,
               AlbumName=x.Album.Name,
               AlbumImg = x.Album.UrlString,
            })
             .ToList();
            if (data.Count == 0)
            {
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            }
            return FormatUtil.JsonFormat(data.Count, "查询成功", 0, data);
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：删除购物车商品删除
        *  return：statu
        */
        [Route("api/DelShoppingCartItems")]
        public string DelShoppingCartItems(Guid id)
        {
            ShoppingCartItems genre = _scit.GetSingle(id);
            int statu = _scit.DeleteAndSave(genre);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：用户名模糊查询
        *  return：json
        */
        [Route("api/Search")]
        [HttpGet]
        public string Search(string name)
        {
            var obj = _sc.GetAll().Select(x => new
            {
                x.Id,
                x.Count,
                UserName = x.User.Name,
            })
            .Where(x => x.UserName.Contains(name))
            .ToList();
            int count = obj.Count();//统计数量
            if (obj.Count == 0)
            {
                return FormatUtil.JsonFormat(count, "没有数据", 1, "");
            }
            return FormatUtil.JsonFormat(count, "成功", 0, obj);
        }


    }
}
