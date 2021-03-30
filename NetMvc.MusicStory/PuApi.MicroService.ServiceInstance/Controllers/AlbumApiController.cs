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
    public class AlbumApiController : ApiController
    {

        private readonly IRepository<Album> _repository;
        MusicStoreDbContext _context;

        public AlbumApiController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }

            _repository = new Repository<Album>(_context);
        }

        public AlbumApiController(IRepository<Album> repository)
        {
            _repository = repository;
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：Api获取专辑数据列表
        *  return：json
        */
        [HttpGet]
        [Route("api/GetAlbumList")]
        public string GetAlbumtList(int page=1, int limit=5)
        {
            //page;//第2页
            //limit;//每页5条数据
            int count = _repository.GetAll().Count();//总条数        
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name,
                x.Price,
                x.UrlString,
                x.OperationDate
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
        *  content：模糊查询
        *  return：json
        */
        [Route("api/Search")]
        [HttpGet]
        public string Search(string name)
        {
            var obj = _repository.GetAll().Select(x => new {
                x.Id,
                x.Name,
                x.Description,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name,
                x.Price,
                x.UrlString,
                x.OperationDate
            })
            .Where(x => x.Name.Contains(name))
            .ToList();
            int count = obj.Count();//统计数量
            if (obj.Count == 0)
            {
                return FormatUtil.JsonFormat(count, "没有数据", 1, "");
            }
            return FormatUtil.JsonFormat(count, "成功", 0, obj);
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：新增专辑数据
        *  return：json
        */
        [Route("api/AddAlbum")]
        [HttpPost]
        public string AddAlbum(
            string name,
            Guid genreid,
            Guid artistid,
            decimal price,
            string description,
            DateTime operationdate)
        {
            Album obj = new Album
            {
                Name = name,
                GenreId=genreid,
                ArtistId=artistid,
                Price=price,
                OperationDate=operationdate,
                Description = description
            };
            int statu = _repository.AddAndSave(obj);
            if (statu!=1)
                return FormatUtil.JsonFormat(0, "添加失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：修改专辑数据
        *  return：statu
        */
        [Route("api/EditAlbum")]
        [HttpPost]
        public string EditAlbum(
                Guid id,
                string name,
                Guid genreid,
                Guid artistid,
                decimal price,
                string description,
                DateTime operationdate)
        {
            var obj = _repository.GetSingle(id);
            obj.Name = name;
            obj.GenreId = genreid;
            obj.ArtistId = artistid;
            obj.Price = price;
            obj.Description = description;
            obj.OperationDate = operationdate;

            int statu = _repository.EditAndSave(obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(statu, "编辑失败", 1, "");
            else
                return FormatUtil.JsonFormat(statu, "编辑成功", 0, "");
        }




        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：专辑数据删除
        *  return：statu
        */
        [Route("api/DelAlbum")]
        public string DelAlbum(Guid id)
        {
            Album genre = _repository.GetSingle(id);
            int statu = _repository.DeleteAndSave(genre);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：专辑下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetAlbumMuen")]
        public string GetAlbumMuen()
        {
            var GenreData = _repository.GetAll().Select(x => new { x.Id, x.Name });
            if (GenreData == null)
            {
             return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            }
            return JsonConvert.SerializeObject(GenreData);
        }



        /*
         *  name:hcw
         *  time: 2020/12/17
         *  content：专辑图片管理
         *  return：json
         */
        [Route("api/AddOrEditImg")]
        [HttpPost]
        public string AddOrEditImg(Guid id, string url)
        {
            var obj = _repository.GetSingle(id);
             obj.UrlString = url;
            int statu = _repository.EditAndSave (obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "添加失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "");
        }


        /*
         *  name:hcw
         *  time: 2020/12/17
         *  content：模糊查询接口
         *  return：json
         */
        public string GetAlbum(string name)
        {
            var obj = _repository.GetAll().Select(x => new {
                x.Id,
                x.Name,
                x.Description,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name,
                x.Price,
                x.UrlString,
                x.OperationDate
            })
            .Where(x => x.Name.Contains(name))
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
