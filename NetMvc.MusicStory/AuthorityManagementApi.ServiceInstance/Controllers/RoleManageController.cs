using AuthorityManagementApi.ServiceInstance.MicroService.ServiceInstance.Unity;
using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Interface.Infrastructure;
using NetMvcMusicStory.Models.MusicStoryDbConnection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;

namespace AuthorityManagementApi.ServiceInstance.Controllers
{
    /*
    *  name:hcw
    *  time: 2021/1/6
    *  content：角色管理
    *  return：json
    */
    public class RoleManageController : ApiController
    {
        private readonly IRepository<Role> _role;
        MusicStoreDbContext _context;

        public RoleManageController()
        {
            if (_context == null)
            {
                _context = new MusicStoreDbContext();
            }
            _role = new Repository<Role>(_context);
        }

        /*
       *  name:hcw
       *  time: 2021/1/6
       *  content：角色数据列表
       *  return：json
       */
        [HttpGet]
        [Route("api/GetRolesList")]
        public string GetRolesList(int page = 1, int limit = 5)
        {
            var DataList = _role
            .GetAll()
            .OrderBy(x => x.Id)
            .Skip(limit * (page - 1)) //跳过指定条数【数量*(页码-1)】
            .Take(limit) //返回指定条数
            .ToList();

            //获取数据总条数
            int count = _role.GetAll().Count();
            if (DataList.Count() == 0)
                return FormatUtil.Result(count, "没有数据", 1, "");
            else
                return FormatUtil.Result(count, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：模糊查询（角色名查询）
        *  return：json
        */
        [Route("api/GetByRoles")]
        public string GetByRoles(string title)
        {
            var DataList =_role.GetAll()
            .Where(x => x.Title.Contains(title))
            .ToList();
            if (DataList.Count == 0)
                return FormatUtil.Result(0, "没有数据", 1, "");
            else
                return FormatUtil.Result(DataList.Count, "查询成功", 0, DataList);
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：添加角色数据
        *  return：json
        */
        [Route("api/AddRoles")]
        [HttpPost]
        public string AddRoles(
            string title
            )
        {
            var name = _role.GetAll()
            .Where(x => x.Title == title)
            .FirstOrDefault();
            if (name != null)
            {
                return FormatUtil.Result(0, "该已存该角色", 1, "");
            }
            Role obj = new Role
            {            
                Title=title,
                Status = 1,//默认启用
                Create_time=DateTime.Now,
                Update_time = DateTime.Now
            };
            int statu = _role.AddAndSave(obj);
            if (statu != 1)
                return FormatUtil.Result(0,"操作失败，请稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"添加成功", 0, "");
        }



        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：修改角色数据
        *  return：json
        */
        [Route("api/EditRoles")]
        [HttpPost]
        public string EditRoles(
            Guid id,
            string title
            )
        {
            var obj = _role.GetAll()
            .Where(x => x.Title == title)
            .FirstOrDefault();
                if (obj != null)
                {
                    return FormatUtil.Result(0, "该已存该角色", 1, "");
                }
                var singleone = _role.GetSingle(id);
                singleone.Title = title;
                singleone.Update_time = DateTime.Now;
                int statu = _role.EditAndSave(singleone);
            if (statu != 1)
                return FormatUtil.Result(0,"编辑失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"编辑成功", 0, "");
        }




        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：角色数据删除
        *  return：json
        */
        [Route("api/DelRoles")]
        [HttpPost]
        public string DelRoles(Guid id)
        {
            Role role = _role.GetSingle(id);
            int statu = _role.DeleteAndSave(role);
            if (statu != 1)
                return FormatUtil.Result(0,"删除失败，稍后重试", 1, "");
            else
                return FormatUtil.Result(0,"删除成功", 0, "");
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：角色开关
        *  return：json
        */
        [Route("api/RoleSwitch")]
        [HttpPost]
        public string RoleSwitch(Guid id,int sta)
        {
            Role role = _role.GetSingle(id);
            role.Status =sta;
            int statu = _role.EditAndSave(role);
            if (statu == 0)
            {
                return FormatUtil.Result(0, "更改失败,稍后重试", 1, "");
            }
            if (sta==1) 
            
                return FormatUtil.Result(0, "已开启", 1, "");
           else
                return FormatUtil.Result(0, "已禁用", 1, "");
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：获取角色下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetRolesMuen")]
        public string GetRolesMuen()
        {
            var GenreData = _role.GetAll()
            .Select(x => new { x.Id, x.Title })
            .ToList() ;
            if (GenreData.Count == 0)
            {
                return FormatUtil.Result(0,"暂无数据，稍后重试", 1, "");
            }
            return JsonConvert.SerializeObject(GenreData);
        }


        /*
        *  name:hcw
        *  time: 2021/1/6
        *  content：获取普通用户下拉菜单数据
        *  return：json
        */
        [HttpPost]
        [Route("api/GetUserRolesMuen")]
        public string GetUserRolesMuen()
        {
            var GenreData = _role.GetAll()
            .Where(x=>x.Title=="普通用户")
            .Select(x => new { x.Id, x.Title })
            .ToList();
            if (GenreData.Count == 0)
            {
                return FormatUtil.Result(0, "暂无数据，稍后重试", 1, "");
            }
            return JsonConvert.SerializeObject(GenreData);
        }

    }
}
