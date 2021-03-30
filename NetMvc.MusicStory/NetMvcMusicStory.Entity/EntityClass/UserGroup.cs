using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{

    /// <summary>
    /// 用户组表
    /// </summary>
    public class UserGroup : IEntity
    {
        //`id` mediumint(8) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
        //`website_id` int(10) NOT NULL DEFAULT '0' COMMENT '站点id，用于多站点，默认0',
        //`title` char(100) NOT NULL DEFAULT '' COMMENT '用户组中文名称',
        //`status` tinyint(1) NOT NULL DEFAULT '1' COMMENT '状态：为1正常，为0禁用',
        //`rules` varchar(600) NOT NULL DEFAULT '' COMMENT '用户组拥有的规则id， 多个规则","隔开',
        //`create_time` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '最后修改时间',
        //`update_time` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
        public UserGroup()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string  Title { get; set; }
        public int Status { get; set; }
        public string  RulesId { get; set; }
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
        public Rule Rule { get; set; }
    }
}