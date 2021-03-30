using AuthorityManagementApi.ServiceInstance.MicroService.ServiceInstance.Unity;
using AuthorityManagementApi.ServiceInstance.Unity.Util;
using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AuthorityManagementApi.ServiceInstance.Controllers
{
    public class LoginController : ApiController
    {

        private readonly IRepository<Role> _role;
        private readonly IRepository<Rule> _rule;
        private readonly IRepository<User> _repository;
        private readonly IRepository<ShoppingCart> _car;
        MusicStoreDbContext _context;

        public LoginController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _repository = new Repository<User>(_context);
            _role = new Repository<Role>(_context);
            _car = new Repository<ShoppingCart>(_context);
            _rule = new Repository<Rule>(_context);
        }


        /*
        *  name:hcw
        *  time: 2021/1/8
        *  content：用户登录校验
        *  return：json(msg,statu,token,Account,Icon)
        */
        [Route("api/LoginCheck")]
        [HttpPost]
        public string LoginCheck(string user, string pass)
        {
            //加密
            string u = Encryption.MD5(user);
            string p = Encryption.MD5(pass);

            User account = _repository.GetAll()
            .Where(x =>x.Is_delete==0&x.Account == u)
            .FirstOrDefault();      

            if (account==null)
            {
                return FormatUtil.JsonFormat("用户名不存在", -1, null, null, null, null);
            }

            if (account.Status == 0)
            {
                return FormatUtil.JsonFormat("无权访问或已被禁用", -1, null, null, null, null);
            }

            //密码校验
            User password = _repository.GetAll()
           .Where(x =>x.Account==u&&x.Password == p)
           .FirstOrDefault();

            if (password == null)
            {
                return FormatUtil.JsonFormat("密码错误", -2, null, null, null, null);
            }

              //角色校验
            Role role = _role.GetAll()
            .Where(x =>x.Status == 1&x.Id== account.RoleId)
            .FirstOrDefault();

            if(role==null) 
            {
                return FormatUtil.JsonFormat("无权访问或已被禁用", -1, null, null, null, null);
            }

            //写入token，账号，头像数据返回
            return FormatUtil.JsonFormat("登录成功", 0, null, account.Account, account.Icon,account.Name);
        }




        /*
        *  name:hcw
        *  time: 2021/1/9
        *  content：管理后台登录
        *  return：json(msg,statu,token,Account,Icon)
        */
        [Route("api/AdminLoginCheck")]
        [HttpPost]
        public string AdminLoginCheck(string user, string pass)
        {
            //加密
            string u = Encryption.MD5(user);
            string p = Encryption.MD5(pass);

            User account = _repository.GetAll()
            .Where(x => x.Is_delete == 0 & x.Account == u)
            .FirstOrDefault();

            if (account == null)
            {
                return FormatUtil.JsonFormat("用户名不存在", -1, null, null, null, null);
            }

            if (account.Status == 0)
            {
                return FormatUtil.JsonFormat("无权访问，或已被禁用", -1, null, null, null, null);
            }

            //密码校验
            User password = _repository.GetAll()
           .Where(x =>x.Account==u&&x.Password == p)
           .FirstOrDefault();

            if (password == null)
            {
                return FormatUtil.JsonFormat("密码错误", -2, null, null, null, null);
            }

            //角色校验
            Role role = _role.GetAll()
            .Where(x => x.Status == 1 & x.Id == account.RoleId)
            .FirstOrDefault();

            if (role == null)
            {
                return FormatUtil.JsonFormat("无权访问或已被禁用", -1, null, null, null, null);
            }

            //规则校验
            Rule rule = _rule.GetAll()
            .Where(x =>x.RoleId==role.Id&x.Status==1)
            .FirstOrDefault();

            if (rule == null)
            {
                return FormatUtil.JsonFormat("没有可操作的权限", -1, null, null, null, null);
            }

            //写入token，账号，头像数据返回
            return FormatUtil.JsonFormat("登录成功", 0, null, account.Account, account.Icon, account.Name);
        }


        /*
        *  name:hcw
        *  time: 2021/1/8
        *  content：获取权限菜单
        *  return：json
        */
        [Route("api/GetRulesMenu")]
        [HttpGet]
        public IHttpActionResult GetRulesMenu(string Acc)
        {
            var account = _repository.GetAll()
            .Where(x => x.Is_delete == 0 & x.Account == Acc)
            .FirstOrDefault();

            //角色校验
            var role = _role.GetAll()
            .Where(x => x.Status == 1 & x.Id == account.RoleId)
            .FirstOrDefault();

            //规则校验
            var rule = _rule.GetAll()
            .Where(x => x.RoleId == role.Id & x.Status == 1)
            .Select(
                x => new{
                x.Title,
                x.MenuUrl,
                RoleName = x.Role.Title
                }
            )
            .ToList();

            Hashtable t5 = new Hashtable();
            List<Hashtable> t4 = new List<Hashtable>();
            Hashtable t3 = new Hashtable();
            Hashtable t2 = new Hashtable();
            Hashtable t = new Hashtable();

            t.Add("title", "首页");
            t.Add("href", "/AdminHomePage/HomePage");
            t2.Add("title", "MusicStore");
            t2.Add("image", "/Content/layuimini/images/logo.png");

            List<object> Menu= new List<object>();

            foreach (var a in rule)
            {
                var obj = new
                {
                    title = a.Title,
                    href = a.MenuUrl,
                    icon = "",
                    target = "_self"
                };
                Menu.Add(obj);
            }

            t5.Add("title", "常规管理");
            t5.Add("icon", "fa fa-address-book");
            t5.Add("href", "");
            t5.Add("target", "_self");
            t5.Add("child", Menu);

            t4.Add(t5);
            t3.Add("homeInfo", t);
            t3.Add("logoInfo", t2);
            t3.Add("menuInfo", t4);
            return Json(t3);
        }


        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content：注册用户
        *  return：json
        */
        [Route("api/AddUser")]
        [HttpPost]
        public string AddUser(
                string name,
                string account,
                string pass,
                string email
            )
        {

            int statu;
            string a = Encryption.MD5(account);
            var selectone = _repository.GetAll()
            .Where(x => x.Account == a)
            .FirstOrDefault();

            if (selectone != null)
            {
                if (selectone.Name == name)
                {
                    return FormatUtil.Status("已有相同昵称存在", 1);
                }
                return FormatUtil.Status("注册失败，已有相同账号存在", 1);
            }
            var role = _role.GetAll()
            .Where(x => x.Title == "普通用户")
            .Select(c => c.Id).FirstOrDefault();
            User obj = new User
            {
                Name = name,
                Account = Encryption.MD5(account),
                Password = Encryption.MD5(pass),
                Email = email,
                Icon = "",
                Status = 1,
                Is_delete = 0,
                RoleId=role,
                Create_time = DateTime.Now,
                Update_time = DateTime.Now,
            };
             statu = _repository.AddAndSave(obj);

            if (statu != 1)
            {
                return FormatUtil.Status("注册失败,请稍后重试", 1);
            }
            else
            {
                 statu = InitShoppingCar(Encryption.MD5(account));//初始化购物车
                if (statu != 1) 
                {
                    return FormatUtil.Status("数据初始化失败，请稍后重试", 1);
                }
                    return FormatUtil.Status("注册成功", 0);
            }
        }


        /*
        *  name:hcw
        *  time: 2021/1/5
        *  content:用户注册初始化购物车
        *  return：statu
        */
        [Route("api/InitShoppingCar")]
        [HttpPost]
        public int InitShoppingCar(string acount)
        {
            var uid = _repository.GetAll()
            .Where(x => x.Account == acount)
            .FirstOrDefault();

            ShoppingCart car = new ShoppingCart
            {
                UserId = uid.Id
            };
            int statu = _car.AddAndSave(car);
            return statu;
        }


        /*
        *  name:hcw
        *  time: 2020/12/28
        *  content:获取用户的Mac地址
        *  return：json
        */
        [Route("api/DeviceCheck")]
        [HttpPost]
        public bool DeviceCheck(string user, string pass)
        {
            //校验设备注册的数量
            return true;
        }
    }
}
