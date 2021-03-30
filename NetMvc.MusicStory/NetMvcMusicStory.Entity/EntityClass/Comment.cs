using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{
    public class Comment : IEntity
    {
        public Comment()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AlbumId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Create_time { get; set; }
        public int Comment_State { get; set; }
        public double Star { get; set; }
        public User User { get; set; }
    }
}