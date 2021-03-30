using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace PuApi.MicroService.ServiceInstance.Controllers
{

    public class GenresApiController : ApiController
    {

        private readonly IRepository<Genre> _repository;
        MusicStoreDbContext _context;
    
        public GenresApiController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }

            _repository = new Repository<Genre>(_context);
        }


        public GenresApiController(IRepository<Genre> repository)
        {
            _repository = repository;
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：Api获取流派数据
        *  return：json
        */
        [HttpGet]
        [Route("api/GetGenreList")]
        public string GetGenreList(int page = 1, int limit = 5)
        {
            
            var DataList = _repository
            .GetAll()
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();

            //获取数据总条数
            int count=_repository.GetAll().Count();
            if (DataList .Count()==0)
                return FormatUtil.JsonFormat(0, "没有数据", 1, DataList);
            else
                return FormatUtil.JsonFormat(count, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：流派数据单挑查询
        *  return：json
        */
        [Route("api/GetByGenre")]
        public string GetByGenre(Guid id)
        {
            //Guid obid=Guid.Parse(id);
            Genre DataList = _repository.GetSingle(id) ;
            if (DataList == null)
                return FormatUtil.JsonFormat(0, "查询失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "查询成功", 0, DataList);
        }




        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：新增流派数据
        *  return：json
        */
        [Route("api/AddGenre")]
        [HttpPost]
        public string AddGenre(string name,string description)
        {
            Genre obj = new Genre
            {
                Name = name,
                Description = description
            };
            int statu = _repository.AddAndSave(obj);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "添加失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "添加成功", 0, "succeed");
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：修改流派数据
        *  return：json
        */
        [Route("api/EditGenre")]
        [HttpPost]
        public string EditGenre(Guid id,string name, string description)
        {
           var obj = _repository.GetAll()
          .Where(x => x.Name == name)
          .FirstOrDefault();
            if (obj != null)
            {
                return FormatUtil.JsonFormat(0, "已有相同名称存在", 1, "");
            }
            var singleone = _repository.GetSingle(id);
            singleone.Name = name;
            singleone.Description =description;
            int statu = _repository.EditAndSave(singleone);

            if (statu != 1)
                return FormatUtil.JsonFormat(0, "编辑失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "编辑成功", 0, "succeed");
        }




        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：流派数据删除
        *  return：json
        */
        [Route("api/DelGenre")]
        [HttpPost]
        public string DelGenre(Guid id)
        {
            Genre genre = _repository.GetSingle(id);
            int statu = _repository.DeleteAndSave(genre);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "succeed");
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取流派下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetGenreMuen")]
        public string GetGenreMuen()
        {
            var GenreData = _repository.GetAll().Select(x => new { x.Id, x.Name });
            if (GenreData == null)
            {
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            }
            return JsonConvert.SerializeObject(GenreData);
        }

    }
}
