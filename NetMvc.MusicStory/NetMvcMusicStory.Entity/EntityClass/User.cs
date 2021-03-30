using NetMvcMusicStory.Interface.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    /*Is_delete: 0未删除，1已删除
     * Status:   0禁用 1正常
     * **/
    public class User : IEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }
        public int? Status { get; set; }
        public int? Is_delete { get; set; }
        public Guid? RoleId { get; set; }
        public DateTime? Create_time { get; set; }
        public DateTime? Update_time { get; set; }
        public Role Role{ get; set; }
    }
}