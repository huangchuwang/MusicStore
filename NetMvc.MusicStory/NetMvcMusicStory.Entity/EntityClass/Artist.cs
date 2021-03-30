
using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{

    /// <summary>
    /// 专辑歌手业务实体
    /// </summary>
    public class Artist: IEntity
    {
        public Artist()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

   
    }
}