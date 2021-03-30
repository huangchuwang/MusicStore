using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Linq;
using System.Web.Http;


namespace PlayMusic.MicroService.ServiceInstance.Controllers
{
    public class PlayMusicAPIController : ApiController
    {

        MusicStoreDbContext _context = new MusicStoreDbContext();
        private readonly IRepository<Music> _repository;
        private readonly IRepository<Comment> _repository1;
        private readonly IRepository<Praise> _repository2;
        private readonly IRepository<User> _repository_u;
        private readonly IRepository<Album> _repository_a;
        public PlayMusicAPIController()
        {
            _repository = new Repository<Music>(_context);
            _repository1 = new Repository<Comment>(_context);
            _repository2 = new Repository<Praise>(_context);
            _repository_u = new Repository<User>(_context);
            _repository_a = new Repository<Album>(_context);
        }

        public PlayMusicAPIController(IRepository<Music> repository)
        {
            _repository = repository;
        }

        [Route("api/GetAllMusicData")]
        [HttpGet]
        public string GetAllMusicData()
        {
            var data = _repository.GetAll();
            return JsonConvert.SerializeObject(data);
        }

        /*
        *  name:lzc
        *  time: 2020/12/25
        *  content：获取音乐数据
        *  return：json
        */
        [Route("api/GetMusicData")]
        [HttpGet]
        public string GetMusicData(Guid id, int page = 1, int limit = 5)
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.AlbumId,
                x.Name,
                x.MusicUrl,
                x.Lrc,
                x.Description,
                x.Create_time,
                x.Update_time,
                AlbumName = x.Album.Name,
                AlbumPlayNumber = x.Album.PlayNumber,
                ArtistName = x.Album.Artist.Name,
                AlbumImgUrl = x.Album.UrlString,
                AlbumDescription = x.Album.Description,
                AlbumDate = x.Album.OperationDate,
                GenreName = x.Album.Genre.Name
            }).Where(x => x.AlbumId == id).ToList();
            int count = data.Count();
            data = data.OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "该专辑不存在或专辑中未收入音乐！", 1, "");
            }
            return FormatUtil.JsonFormat(count, "查询专辑成功！", 0, data); ;
        }


        /*
        *  name:lzc
        *  time: 2020/12/30
        *  content：获取评论数据
        *  return：json
        */
        [Route("api/GetCommentData")]
        [HttpGet]
        public string GetCommentData(Guid id)
        {
            var data = _repository1.GetAll().Select(x => new
            {
                x.Id,
                x.Content,
                x.AlbumId,
                x.Create_time,
                x.Comment_State,
                x.Star,
                UserAccount = x.User.Account,
                UserName = x.User.Name,
                UserIcon = x.User.Icon
            }).Where(x => x.AlbumId == id && x.Comment_State == 1).OrderByDescending(x => x.Create_time).ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "该专辑暂无评论！", 1, "");
            }
            return FormatUtil.JsonFormat(data.Count(), "查询评论成功！", 0, data); ;
        }

        /*
        *  name:lzc
        *  time: 2021/01/04
        *  content：发表评论
        *  return：string
        */
        [Route("api/GetCommentData")]
        [HttpPost]
        public string CreateComment(Guid albumid, string account, string content, double star)
        {
            try
            {
                var user = _repository_u.GetAll().Where(x => x.Account == account).FirstOrDefault();
                Comment entity = new Comment()
                {
                    AlbumId = albumid,
                    UserId = user.Id,
                    Content = content,
                    Create_time = DateTime.Now,
                    Comment_State = 1,
                    Star = star
                };
                _repository1.AddAndSave(entity);
                return "发表评论成功！";
            }
            catch
            {
                return "发表评论失败！";
            }
        }

        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：点赞专辑
        *  return：string
        */
        [Route("api/HandlePraise")]
        [HttpPost]
        public string HandlePraise(Guid albumid, string account)
        {
            var user = _repository_u.GetAll().Where(x => x.Account == account).FirstOrDefault();
            var praise = _repository2.GetAll().Where(x => x.UserId == user.Id && x.AlbumId == albumid).FirstOrDefault();
            if (praise == null)
            {
                Praise entity = new Praise()
                {
                    AlbumId = albumid,
                    UserId = user.Id,
                    Update_time = DateTime.Now,
                    Praise_State = 1
                };
                entity.Praise_State = 1;
                _repository2.AddAndSave(entity);
                return "创建点赞成功！";
            }
            else
            {
                if (praise.Praise_State == 1)
                    praise.Praise_State = 0;
                else
                    praise.Praise_State = 1;
                _repository2.EditAndSave(praise);
                return "修改点赞成功！";
            }
        }

        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：点赞数量
        *  return：int
        */
        [Route("api/PraiseCount")]
        [HttpPost]
        public int PraiseCount(Guid albumid)
        {
            var count = _repository2.GetAll().Where(x => x.AlbumId == albumid && x.Praise_State == 1).Count();
            return count;
        }


        /*
        *  name:lzc
        *  time: 2021/01/05
        *  content：点赞检索
        *  return：bool
        */
        [Route("api/Inspect_Praise")]
        [HttpPost]
        public bool Inspect_Praise(Guid albumid, string account)
        {
            var user = _repository_u.GetAll().Where(x => x.Account == account).FirstOrDefault();
            var praise = _repository2.GetAll().Where(x => x.UserId == user.Id && x.AlbumId == albumid).FirstOrDefault();
            if (praise == null || praise.Praise_State == 0)
                return false;
            else
                return true;
        }


        /*
        *  name:lzc
        *  time: 2021/01/12
        *  content：增加专辑播放量
        *  return：string
        */
        [Route("api/PlayNumberAdd")]
        [HttpPost]
        public string PlayNumberAdd(Guid albumid)
        {
            var user = _repository_a.GetAll().Where(x => x.Id == albumid).FirstOrDefault();
            user.PlayNumber += 1;
            _repository_a.EditAndSave(user);
            return "播放量操作成功";
        }
    }
}