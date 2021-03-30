using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{

    /*
     * Title：角色名称
     * Status：状态标识：
     *      0：禁用
     *      1：正常      
     */

    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : IEntity
    {    
        public Role()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public int Status { get; set; }
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }

    }
}