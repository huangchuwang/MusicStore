using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PuApi.MicroService.ServiceInstance.Controllers
{
    public class HomeController : ApiController
    {


        private readonly IRepository<Genre> _genre;
        private readonly IRepository<Album> _album;
        MusicStoreDbContext _context;

        public HomeController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _genre = new Repository<Genre>(_context);
            _album = new Repository<Album>(_context);
        }




        /*
        *  name:hcw
        *  time: 2020/12/19
        *  content：图形化统计数据接口
        *  return：json
        */
        [HttpPost]
        [Route("api/GenreByCountList")]
        public string GenreByCountList()
        {
            //给Album的GenreId分组统计
            var list = _album.GetAll()
            .GroupBy(b => b.GenreId)
            .Select(g => new { genreid = g.Key, count = g.Count() })
            .OrderByDescending(t => t.count).ToArray();

            List<object> li = new List<object>();
            foreach (var d in list)     //遍历查询Genreid命名 写入集合返回
            {
                var str = d.genreid.ToString();
                Guid GenreGuid = Guid.Parse(str);
                Genre obj = _genre.GetSingle(GenreGuid);
                li.Add(new { name = obj.Name, count = d.count, max = 5 });
            }
            return JsonConvert.SerializeObject(li);
        }


    }
}
