using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Linq;
using System.Web.Http;

namespace PuApi.MicroService.ServiceInstance.Controllers
{

    public class MusicApiController : ApiController
    {
        private readonly IRepository<Music> _repository;
        MusicStoreDbContext _context;
    
        public MusicApiController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }

            _repository = new Repository<Music>(_context);
        }


        public MusicApiController(IRepository<Music> repository)
        {
            _repository = repository;
        }



        /*
        *  name:hcw
        *  time: 2020/12/17
        *  content：Api获取音乐数据
        *  return：json
        */
        [HttpGet]
        [Route("api/GetMusicList")]
        public string GetMusicList(int page = 1, int limit = 5)
        {
            var MusicData = _repository.GetAll()
            .Select(x => new {
                x.Id,
                x.Name,
                x.MusicUrl,
                x.Lrc,
                x.Description,
                x.Create_time,
                x.Update_time,
                AlbumName = x.Album.Name
            })
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();
            if (MusicData == null)
            {
                return FormatUtil.JsonFormat(0, "没有数据", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询成功", 0, MusicData);
        }


        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：获取专辑下的所有音乐数据
        *  return：json
        */
        [HttpGet]
        [Route("api/GetAlbumByMusicList")]
        public string  GetAlbumByMusicList(Guid albumid)
        {
            var DataList = _repository.GetAll()
                .Select(x=>new {
                    x.Name,
                    x.Description,
                    x.MusicUrl,
                    x.AlbumId})
              .Where(x =>x.AlbumId==albumid).ToList();
            if (DataList == null)
                return FormatUtil.JsonFormat(0, "查询失败", 0, "");
            else
                return FormatUtil.JsonFormat(1, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：模糊查询音乐数据
        *  return：json
        */
        [Route("api/GetByMusic")]
        [HttpGet ]
        public string  GetByMusic(string name)
        {
            var obj = _repository.GetAll().Select(x => new {
                x.Id,
                x.Name,
                x.Description,
                ArtistName = x.Album.Name,
                x.MusicUrl,
                x.Create_time
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
        *  content：添加音乐数据
        *  return：json
        */
        [Route("api/AddMusic")]
        [HttpPost]
        public string AddMusic(Guid albumid, string name, string musicurl, string description)
        {
            var obj = _repository.GetAll()
            .Where(x => x.Name == name)
            .FirstOrDefault();
            if (obj != null)
            {
                return FormatUtil.JsonFormat(0, "已有相同音乐名称存在", 1, "");
            }
            Music music = new Music
            {
                Name = name,
                AlbumId = albumid,
                MusicUrl = musicurl,
                Create_time = DateTime.Now.ToLocalTime(),
                Update_time = DateTime.Now.ToLocalTime(),
                Description = description
            };
            int statu = _repository.AddAndSave(music);
            if (statu != 1)
            {
                return FormatUtil.JsonFormat(0, "添加失败", 1, "");
            }
            return FormatUtil.JsonFormat(0, "添加成功", 0, "");
        }


        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：修改音乐数据
        *  return：json
        */

        [Route("api/EditMusic")]
        [HttpPost]
        public string EditMusic(Guid id, Guid albumid, string name, string musicurl, string description)
        {
            Music music = _repository.GetSingle(id);
            music.Name = name;
            music.AlbumId = albumid;
            music.MusicUrl = musicurl;
            music.Description = description;
            music.Create_time = DateTime.Now.ToLocalTime();

            int statu = _repository.EditAndSave(music);
            if (statu == 0)
            {
                return FormatUtil.JsonFormat(0, "编辑失败", 1, "");
            }
            return FormatUtil.JsonFormat(0, "编辑成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2020/12/15
        *  content：删除音乐
        *  return：json
        */
        [Route("api/DelMusic")]
        [HttpPost]
        public string DelMusic(Guid id)
        {
            Music genre = _repository.GetSingle(id);
            int a = _repository.DeleteAndSave(genre);
            if (a != 1)
            {
                return FormatUtil.JsonFormat(0, "删除失败", 1, "");
            }
            return FormatUtil.JsonFormat(0, "删除成功", 0, "");
        }



        /*
      *  name:hcw
      *  time: 2020/12/17
      *  content：添加歌词
      *  return：json
      */
        [Route("api/AddLrc")]
        [HttpPost]
        public string AddLrc(Guid id, string url)
        {
            Music music = _repository.GetSingle(id);
            music.Lrc = url;
            music.Update_time = DateTime.Now.ToLocalTime();

            int statu = _repository.EditAndSave(music);
            if (statu == 0)
            {
                return FormatUtil.JsonFormat(0, "导入失败", 1, "");
            }
            return FormatUtil.JsonFormat(0, "导入成功", 0, "");
        }

    }
}
