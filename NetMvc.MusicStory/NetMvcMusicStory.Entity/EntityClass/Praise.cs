using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{
    public class Praise : IEntity
    {
        public Praise()
        {
            this.Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Update_time { get; set; }
        public int Praise_State { get; set; }
    }
}