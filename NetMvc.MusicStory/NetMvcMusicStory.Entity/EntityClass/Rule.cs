using NetMvcMusicStory.Interface.Infrastructure;
using System;
namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 规则表
    /// </summary>
    /*
     *  Title：规则标题
     *  Status：状态
     *  Front：按钮标识 del,create,search,delete,0
     *  MenuUrl：菜单 
     *  Status :0禁用 1启用
     ***/

    public class Rule : IEntity
    {
        public Rule()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public int  Status { get; set; }
        public string Front { get; set; }
        public string MenuUrl { get; set; }
        public Guid RoleId { get; set; }      
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
        public Role Role { get; set; }
    }
}