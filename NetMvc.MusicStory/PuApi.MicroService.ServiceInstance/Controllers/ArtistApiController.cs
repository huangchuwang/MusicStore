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
    public class ArtistApiController : ApiController
    {
        private readonly IRepository<Artist> _repository;
        MusicStoreDbContext _context;

        public ArtistApiController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }

            _repository = new Repository<Artist>(_context);
        }

        public ArtistApiController(IRepository<Artist> repository)
        {
            _repository = repository;
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：Api获取歌手数据列表
        *  return：json
        */
        [HttpGet]
        [Route("api/GetArtistList")]
        public string GetArtistList(int page = 1, int limit = 5)
        {

            var DataList = _repository
            .GetAll()
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();

            //获取数据总条数
            int count = _repository.GetAll().Count();
            if (DataList.Count() == 0)
                return FormatUtil.JsonFormat(0, "没有数据", 1, DataList);
            else
                return FormatUtil.JsonFormat(count, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：歌手数据单挑查询
        *  return：json
        */
        [Route("api/GetByArtist")]
        public string GetByArtist(Guid id)
        {
            //Guid obid=Guid.Parse(id);
            Artist DataList = _repository.GetSingle(id) ;
            if (DataList== null)
                return FormatUtil.JsonFormat(0, "查询失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：新增歌手数据
        *  return：json
        */
        [Route("api/AddArtist")]
        [HttpPost]
        public string AddArtist(string name, string description)
        {
            Artist obj = new Artist
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
        *  content：修改歌手数据API
        *  return：json
        */
        [Route("api/EditArtist")]
        [HttpPost]
        public string EditArtist(Guid id, string name, string description)
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
            singleone.Description = description;
            int statu = _repository.EditAndSave(singleone);

            if (statu != 1)
                return FormatUtil.JsonFormat(0, "编辑失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "编辑成功", 0, "succeed");
        }




        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：歌手数据删除API
        *  return：json
        */
        [Route("api/DelArtist")]
        [HttpPost]
        public string DelArtist(Guid id)
        {
            Artist genre = _repository.GetSingle(id);
            int statu = _repository.DeleteAndSave(genre);
            if (statu != 1)
                return FormatUtil.JsonFormat(0, "删除失败", 1, "");
            else
                return FormatUtil.JsonFormat(1, "删除成功", 0, "succeed");
        }


        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：获取歌手下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetArtistMuen")]
        public string GetArtistMuen()
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
