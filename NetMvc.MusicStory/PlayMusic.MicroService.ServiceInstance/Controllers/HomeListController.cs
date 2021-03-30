using Microsoft.Ajax.Utilities;
using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using PuApi.MicroService.ServiceInstance.Unity;
using System;
using System.Collections;
using System.Linq;
using System.Web.Http;

namespace PlayMusic.MicroService.ServiceInstance.Controllers
{
    public class HomeListAPIController : ApiController
    {
        MusicStoreDbContext _context = new MusicStoreDbContext();
        private readonly IRepository<Album> _repository;
        private readonly IRepository<Comment> _repository_c;
        private readonly IRepository<Praise> _repository_p;
        public HomeListAPIController()
        {
            _repository = new Repository<Album>(_context);
            _repository_c = new Repository<Comment>(_context);
            _repository_p = new Repository<Praise>(_context);
        }

        public HomeListAPIController(IRepository<Album> repository)
        {
            _repository = repository;
        }
        
        //最新榜单
        [Route("api/Newest_list")]
        [HttpGet]
        public string Newest_list()
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).OrderByDescending(x => x.OperationDate).ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询最新榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询最新榜单成功！", 0, data); ;
        }

        //流量榜单
        [Route("api/Flow_list")]
        [HttpGet]
        public string Flow_list()
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).OrderByDescending(x => x.PlayNumber).ToList().Take(4);
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询流量榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询流量榜单成功！", 0, data); ;
        }

        //热门榜单
        [Route("api/Hot_list")]
        [HttpGet]
        public string Hot_list()
        {
            Guid[] albumid = Hot_Sort();
            Guid id = albumid[0];
            Guid id2 = albumid[1];
            Guid id3 = albumid[2];
            Guid id4 = albumid[3];
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).OrderByDescending(x => x.Id == id).ThenByDescending(x => x.Id == id2).ThenByDescending(x => x.Id == id3).ThenByDescending(x => x.Id == id4).ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询热门榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询热门榜单成功！", 0, data);
        }

        public Guid[] Hot_Sort()
        {
            //获取评论数据并分组
            var data = _repository_c.GetAll().Select(x => new
            {
                x.AlbumId
            }).GroupBy(x => x.AlbumId).ToArray();

            //计算各个专辑评论数量
            int[] arr = new int[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                arr[i] = data[i].Count();
            }

            //根据评论数量排序
            int[] arr2 = new int[4];
            for(int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr.Max();
                for (int x = 0; x < arr.Length; x++)
                {
                    if (arr[x] == arr.Max())
                    {
                        arr[x] = 0;
                        break;
                    }
                }
            }

            //根据评论排序进行组合ID数组
            bool tf = true;
            Guid[] albumid = new Guid[4];
            for(int i = 0; i < albumid.Length; i++)
            {
                for(int x = 0; x < data.Count(); x++)
                {
                    for (int n = 0; n < albumid.Length; n++)
                    {
                        if (data[x].Key == albumid[n])
                        {
                            tf = false;
                        }
                    }
                    if (arr2[i] == data[x].Count())
                    {
                        if (data[x].Key != albumid[0] && tf)
                            albumid[i] = data[x].Key;
                    }
                    tf = true;
                }
            }
            return albumid;
        }

        //好评榜单
        [Route("api/Evaluate_list")]
        [HttpGet]
        public string Evaluate_list()
        {
            Guid[] albumid = Evaluate_Sort();
            Guid id = albumid[0];
            Guid id2 = albumid[1];
            Guid id3 = albumid[2];
            Guid id4 = albumid[3];
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).OrderByDescending(x => x.Id == id).ThenByDescending(x => x.Id == id2).ThenByDescending(x => x.Id == id3).ThenByDescending(x => x.Id == id4).ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询好评榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询好评榜单成功！", 0, data);
        }

        public Guid[] Evaluate_Sort()
        {
            //获取评论数据并分组
            var data = _repository_c.GetAll().Select(x => new
            {
                x.AlbumId,
                x.Star
            }).GroupBy(x => x.AlbumId).ToList();

            //计算各个专辑评星平均分
            double[] arr = new double[data.Count()];
            string[,] arrkey = new string[data.Count(), 2];
            for (int i = 0; i < data.Count(); i++)
            {
                double star = 0;
                for (int x = 0; x < data[i].Count(); x++)
                {
                    star += data[i].Skip(x).FirstOrDefault().Star;
                }
                arr[i] = star / data[i].Count();
                arrkey[i, 0] = arr[i].ToString();
                arrkey[i, 1] = data[i].Key.ToString();
            }

            //根据评论数量排序
            double[] arr2 = new double[4];
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr.Max();
                for (int x = 0; x < arr.Length; x++)
                {
                    if (arr[x] == arr.Max())
                    {
                        arr[x] = 0;
                        break;
                    }
                }
            }


            //根据评论排序进行组合ID数组
            Guid[] albumid = new Guid[4];
            bool tf = true;
            for (int i = 0; i < albumid.Length; i++)
            {
                for (int x = 0; x < data.Count(); x++)
                {
                    for (int n = 0; n < albumid.Length; n++)
                    {
                        if (new Guid(arrkey[x, 1]) == albumid[n])
                        {
                            tf = false;
                        } 
                    }
                    if (arr2[i].ToString() == arrkey[x, 0] && tf)
                    {
                        albumid[i] = new Guid(arrkey[x, 1]);
                        continue;
                    }
                    tf = true;
                }
            }
            return albumid;
        }

        //点赞榜单
        [Route("api/Praise_list")]
        [HttpGet]
        public string Praise_list()
        {
            Guid[] albumid = Parise_Sort();
            Guid id = albumid[0];
            Guid id2 = albumid[1];
            Guid id3 = albumid[2];
            Guid id4 = albumid[3];
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).OrderByDescending(x => x.Id == id).ThenByDescending(x => x.Id == id2).ThenByDescending(x => x.Id == id3).ThenByDescending(x => x.Id == id4).ToList();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询热门榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询热门榜单成功！", 0, data);
        }

        public Guid[] Parise_Sort()
        {
            //获取评论数据并分组
            var data = _repository_p.GetAll().Select(x => new
            {
                x.AlbumId
            }).GroupBy(x => x.AlbumId).ToArray();

            //计算各个专辑点赞数量
            int[] arr = new int[data.Count()];
            for (int i = 0; i < data.Count(); i++)
            {
                arr[i] = data[i].Count();
            }

            //根据评论数量排序
            int[] arr2 = new int[4];
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr.Max();
                for (int x = 0; x < arr.Length; x++)
                {
                    if (arr[x] == arr.Max())
                    {
                        arr[x] = 0;
                        break;
                    }
                }
            }

            //根据评论排序进行组合ID数组
            bool tf = true;
            Guid[] albumid = new Guid[4];
            for (int i = 0; i < albumid.Length; i++)
            {
                for (int x = 0; x < data.Count(); x++)
                {
                    for (int n = 0; n < albumid.Length; n++)
                    {
                        if (data[x].Key == albumid[n])
                        {
                            tf = false;
                        }
                    }
                    if (arr2[i] == data[x].Count())
                    {
                        if (data[x].Key != albumid[0] && tf)
                            albumid[i] = data[x].Key;
                    }
                    tf = true;
                }
            }
            return albumid;
        }

        //流派榜单
        [Route("api/Genre_list")]
        [HttpPost]
        public string Genre_list(string genrename)
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.GenreId,
                x.ArtistId,
                x.Name,
                x.Price,
                x.UrlString,
                x.OperationDate,
                x.Description,
                x.PlayNumber,
                GenreName = x.Genre.Name,
                ArtistName = x.Artist.Name
            }).Where(x => x.GenreName == genrename).OrderByDescending(x => x.PlayNumber).FirstOrDefault();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询流派榜单失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询流派榜单成功！", 0, data);
        }
    }
}