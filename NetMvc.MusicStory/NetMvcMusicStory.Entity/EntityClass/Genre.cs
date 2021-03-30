using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 流派业务实体
    /// </summary>
    public class Genre: IEntity
    {
        public Genre()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        
    }
}