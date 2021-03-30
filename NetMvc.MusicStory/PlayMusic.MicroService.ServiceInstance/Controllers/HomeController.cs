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
    public class HomeAPIController : ApiController
    {
        MusicStoreDbContext _context = new MusicStoreDbContext();
        private readonly IRepository<User> _repository;
        public HomeAPIController()
        {
            _repository = new Repository<User>(_context);
        }

        public HomeAPIController(IRepository<User> repository)
        {
            _repository = repository;
        }


        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：个人信息
        *  return：string
        */
        [Route("api/GetUser")]
        [HttpGet]
        public string GetUser(string account)
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.Name,
                x.Account,
                x.Email,
                x.Icon,
                x.Status,
                x.Is_delete,
                x.Create_time
            }).Where(x => x.Account == account).FirstOrDefault();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "查询用户信息失败！", 1, "");
            }
            return FormatUtil.JsonFormat(0, "查询用户信息成功！", 0, data);
        }


        /*
        *  name:lzc
        *  time: 2021/1/12
        *  content：个人信息
        *  return：string
        */
        [Route("api/Edit_User")]
        [HttpPost]
        public string Edit_User(string account, string name, string email, string icon)
        {
            var data = _repository.GetAll().Select(x => new
            {
                x.Id,
                x.Name,
                x.Account,
                x.Password,
                x.Email,
                x.Icon,
                x.Status,
                x.Is_delete,
                x.RoleId,
                x.Create_time,
                x.Update_time
            }).Where(x => x.Account == account).FirstOrDefault();
            if (data == null)
            {
                return FormatUtil.JsonFormat(0, "未查询到用户信息！", 1, "");
            }
            User user = new User()
            {
                Id = data.Id,
                Name = name,
                Account = data.Account,
                Password=data.Password,
                Icon = "../Content/UploaFile/UserIcon/"+icon,
                Email = email,
                Status = data.Status,
                Is_delete = data.Is_delete,
                RoleId=data.RoleId,
                Create_time = data.Create_time,
                Update_time = DateTime.Now
            };
            _repository.EditAndSave(user);
            return FormatUtil.JsonFormat(0, "修改用户信息成功！", 0, user);
        }
    }
}
