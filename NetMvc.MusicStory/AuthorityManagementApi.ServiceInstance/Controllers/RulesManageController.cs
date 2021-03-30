using AuthorityManagementApi.ServiceInstance.MicroService.ServiceInstance.Unity;
using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace AuthorityManagementApi.ServiceInstance.Controllers
{
    /*
    *  name:hcw
    *  time: 2021/1/6
    *  content：规则管理
    *  return：json(msg,statu,token,Account,Icon)
    */
    public class RulesManageController : ApiController
    {

        private readonly IRepository<Rule> _rules;
        private readonly IRepository<Role> _role;
        private readonly IRepository<User> _user;
        MusicStoreDbContext _context;

        public RulesManageController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _rules = new Repository<Rule>(_context);
            _role = new Repository<Role>(_context);
            _user = new Repository<User>(_context);
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：规则数据列表
        *  return：json
        */
        [HttpGet]
        [Route("api/GetRulesList")]
        public string GetRulesList(int page = 1, int limit = 5)
        {
            var DataList =
              _rules.GetAll().
             Select(x => new
             {
                 x.Id,
                 x.Title,
                 x.MenuUrl,
                 x.Status,
                 RoleName = x.Role.Title,
                 x.Create_time,
                 x.Update_time
             })
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();

            //获取数据总条数
            int count = _rules.GetAll().Count();
            if (DataList.Count() == 0)
                return FormatUtil.Result(DataList.Count, "没有数据",1,"");
            else
                return FormatUtil.Result(DataList.Count, "查询成功",0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2021/1/12
        *  content：基于用户的访问控制
        *  return：json
        */
        [HttpGet]
        [Route("api/GetUserRulesList")]
        public string GetUserRulesList(int page = 1, int limit = 5)
        {
            var DataList =
              _rules.GetAll()
             .Select(x => new
             {
                 x.Id,
                 x.Title,
                 x.MenuUrl,
                 x.Status,
                 RoleName = x.Role.Title,             
                 x.Create_time,
                 x.Update_time
             })
             .Where(x=>x.RoleName=="普通用户")
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();

            //获取数据总条数
            if (DataList.Count() == 0)
                return FormatUtil.Result(DataList.Count, "没有数据", 1, "");
            else
                return FormatUtil.Result(DataList.Count, "查询成功", 0, DataList);
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：模糊查询（规则名查询）
        *  return：json
        */
        [Route("api/GetByRules")]
        public string GetByRules(string title)
        {
            var DataList = _rules.GetAll()
            .Where(x => x.Title.Contains(title))
            .ToList();
            if (DataList.Count == 0)
                return FormatUtil.Result(0,"没有数据",1, "");
            else
                return FormatUtil.Result(DataList.Count,"查询成功", 0, DataList);
        }

        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：新增规则
        *  return：json
        */
        [Route("api/AddRules")]
        [HttpPost]
        public string AddRules(
            string  title,
            Guid roleid ,
            string menuurl
            )
        {        
            Rule obj = new Rule
            {
               Title=title,
               Status=1,//默认启用
               RoleId =roleid,
               Front=null,
               MenuUrl =menuurl,
              Create_time =DateTime.Now,
              Update_time = DateTime.Now
            };
            int statu = _rules.AddAndSave(obj);
            if (statu != 1)
                return FormatUtil.Result(0,"添加失败,请稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"添加成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：修改规则
        *  return：json
        */
        [Route("api/EditRules")]
        [HttpPost]
        public string EditRules(
            Guid id,
            string title,
            Guid roleid,
            string menuurl
            )
        {
            var obj = _rules.GetAll()
           .Where(x => x.Title == title)
           .FirstOrDefault();
            if (obj != null)
            {
                return FormatUtil.Result(0,"该已存在规则", 1, "");
            }
            var singleone = _rules.GetSingle(id);
            singleone.Title = title;
            singleone.RoleId = roleid;
            singleone.MenuUrl = menuurl;
            singleone.Update_time = DateTime.Now;
            int statu = _rules.EditAndSave(singleone);
            if (statu != 1)
                return FormatUtil.Result(0,"编辑失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"编辑成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：规则开关
        *  return：json
        */
        [Route("api/RuleSwitch")]
        [HttpPost]
        public string RuleSwitch(Guid id, int sta)
        {
            Rule rules = _rules.GetSingle(id);
            rules.Status = sta;
            int statu = _rules.EditAndSave(rules);
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
        *  content：规则数据删除
        *  return：json
        */
        [Route("api/DelRules")]
        [HttpPost]
        public string DelRules(Guid id)
        {
            Rule genre = _rules.GetSingle(id);
            int statu = _rules.DeleteAndSave(genre);
            if (statu != 1)
                return FormatUtil.Result(0,"删除失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"删除成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：获取普通用户访问规则
        *  return：json
        */
        [HttpPost]
        [Route("api/GetRuleMenu")]
        public IHttpActionResult GetRuleMenu(string Account)
        {
            if (Account == null || Account == "")
            {
                return NotFound();    
            }
           var u= _user.GetAll().Where(x=>x.Account==Account).FirstOrDefault();
            var role = _role.GetAll().Where(x => x.Id==u.RoleId).FirstOrDefault();       
            var GenreData = _rules.GetAll()
            .Where(x => x.Status == 1&&x.RoleId==role.Id)
            .Select(x => new {
                x.Id, 
                x.MenuUrl,
                RoleName=x.Role.Title
            })
            .ToList();
            return Json(GenreData);
       }

    }
}
