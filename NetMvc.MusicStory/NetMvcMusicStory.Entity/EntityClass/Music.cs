using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{ 
    /// <summary>
    /// 音乐单曲业务实体
    /// </summary>
    public class Music: IEntity
    {
        public Music()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public string Name { get; set; }
        public string MusicUrl { get; set; } //音乐地址
        public string Lrc { get; set; }//歌词文件
        public string Description { get; set; }
        public Album Album { get; set; }
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
    }
}