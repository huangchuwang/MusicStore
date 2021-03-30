using AuthorityManagementApi.ServiceInstance.MicroService.ServiceInstance.Unity;
using AuthorityManagementApi.ServiceInstance.Unity.Util;
using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;

namespace AuthorityManagementApi.ServiceInstance.Controllers
{
    public class UserManageController : ApiController
    {
        private readonly IRepository<User> _user;
        MusicStoreDbContext _context;

        public UserManageController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _user = new Repository<User>(_context);
        }

        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：用户数据列表
        *  return：json
        */
        [HttpGet]
        [Route("api/GetUsersList")]
        public string GetUsersList(int page = 1, int limit = 5)
        {
            var DataList =
             _user.GetAll().
             Select(x => new
             {
                 x.Id,
                 x.Name,
                 x.Email,
                 x.Icon,
                 x.Status,
                 x.Is_delete,
                 RoleName = x.Role.Title,
                 x.Create_time,
                 x.Update_time
             })
             .Where(x=>x.Is_delete==0)//未删除
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1))
            .Take(limit)
            .ToList();

            //获取数据总条数
            int count = _user.GetAll().Count();
            if (DataList.Count() == 0)
                return FormatUtil.Result(count, "没有数据", 1, "");
            else
                return FormatUtil.Result(count, "查询成功", 0, DataList);
        }

        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：用户开关
        *  return：json
        */
        [Route("api/Switch")]
        [HttpPost]
        public string UsersSwitch(Guid id, int sta)
        {
            User role =_user.GetSingle(id);
            role.Status = sta;
            int statu = _user.EditAndSave(role);
            if (statu == 0)
            {
                return FormatUtil.Result(0, "更改失败,稍后重试", 1, "");
            }
            if (sta == 1)

                return FormatUtil.Result(0, "已开启", 1, "");
            else
                return FormatUtil.Result(0, "已禁用", 1, "");
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：用户名模糊查询
        *  return：json
        */
        [HttpGet]
        [Route("api/GetByUsers")]
        public string GetByUsers(string name)
        {
            var DataList =
           _user.GetAll().
           Select(x => new
           {
               x.Id,
               x.Name,
               x.Email,
               x.Icon,
               x.Status,
               x.Is_delete,
               RoleName = x.Role.Title,
               x.Create_time,
               x.Update_time
           })
            .Where(x => x.Name.Contains(name))
           .ToList();
            if (DataList.Count == 0)
                return FormatUtil.Result(DataList.Count, "查询失败", 0, "");
            else
                return FormatUtil.Result(DataList.Count, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：新增用户数据
        *  return：json
        */
        [HttpPost]
        [Route("api/AddAccount")]
      public  string AddAccount(
            string name,
            string account,
            string password,
            string email,
            Guid roleid
            )
        {
            User obj = new User
            {
                Name = name,
                Account = Encryption.MD5(account),
                Password = Encryption.MD5(password),
                RoleId=roleid,
                Email = email,
                Status = 1, //正常状态
                Is_delete=0,//默认值
                Icon = "../Content/UploaFile/UserIcon/userdefult.jpg",//初始化头像
                Create_time = DateTime.Now,
                Update_time = DateTime.Now
            };
            int statu = _user.AddAndSave(obj);
            
            int initsc = new LoginController().InitShoppingCar(Encryption.MD5(account));//初始化购物车
            if (statu!=1||initsc!=1)
                return FormatUtil.Result(0, "添加失败，稍后重试", 1, "");
            else  
            return FormatUtil.Result(0, "添加成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：修改用户数据
        *  return：json
        */
        [Route("api/EditUsers")]
        [HttpPost]
        public string EditUsers(
            Guid id,
            string name,
            string account,
            string password,
            string email,
            Guid roleid
            //string icon
            //int status
            )
        {
            string u = Encryption.MD5(account);
            string p = Encryption.MD5(password);
            var obj = _user.GetAll()
           .Where(x => x.Account == u)
           .FirstOrDefault();

            if (obj != null)
            {
                return FormatUtil.Result(0, "该账号已存在", 1, "");
            }
            var singleone = _user.GetSingle(id);
            singleone.Name = name;
            singleone.Account = u;
            singleone.Password = p;
            singleone.Email = email;
            singleone.RoleId = roleid;
            //singleone.Icon= "../Content/UploaFile/UserIcon/userdefult.jpg";//默认头像
            //singleone.Status= status;
            singleone.Update_time = DateTime.Now;
            int statu = _user.EditAndSave(singleone);
            if (statu != 1)
                return FormatUtil.Result(0, "编辑失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0, "编辑成功", 0, "");
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：用户数据删除
        *  return：json
        */
        [Route("api/DelUsers")]
        [HttpPost]
        public string DelUsers(Guid id)
        {
            User user = _user.GetSingle(id);
            user.Is_delete = 1;//软删除
            int statu = _user.EditAndSave(user);
            if (statu != 1)
                return FormatUtil.Result(0, "删除失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0, "删除成功", 0, "");
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：角色开关
        *  return：json
        */
        [Route("api/UserSwitch")]
        [HttpPost]
        public string UserSwitch(Guid id, int sta)
        {
            User user = _user.GetSingle(id);
            user.Status = sta;
            int statu = _user.EditAndSave(user);
            if (statu == 0)
            {
                return FormatUtil.Result(0, "更改失败,稍后重试", 1, "");
            }
            if (sta == 1)

                return FormatUtil.Result(0, "已开启", 1, "");
            else
                return FormatUtil.Result(0, "已禁用", 1, "");
        }

        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：获取用户下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetUsersMuen")]
        public string GetUsersMuen()
        {
            var GenreData = _user.GetAll()
            .Where(x => x.Is_delete == 0)
            .Select(x => new { x.Id, x.Name })
            .ToList();
            if (GenreData.Count == 0)
            {
                return FormatUtil.Result(0, "暂无数据", 1, "");
            }
            return JsonConvert.SerializeObject(GenreData);
        }
    }
}