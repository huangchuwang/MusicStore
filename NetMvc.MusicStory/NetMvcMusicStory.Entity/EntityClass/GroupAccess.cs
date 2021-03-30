using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 用户组明细表
    /// </summary>
    public class GroupAccess : IEntity
    {
        // `uid` mediumint(8) unsigned NOT NULL COMMENT '用户id',
        //`group_id` mediumint(8) unsigned NOT NULL COMMENT '用户组id',
        //`create_time` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '最后修改时间',
        //`update_time` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
        public GroupAccess()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid UserGroupId { get; set; }
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
        public User User{ get; set; }
        public UserGroup UserGroup { get; set; }
    }
}