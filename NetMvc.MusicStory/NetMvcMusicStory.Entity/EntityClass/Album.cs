using NetMvcMusicStory.Interface.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 专辑业务实体
    /// </summary>
    public class Album : IEntity
    {
        public Album()
        {
            this.Id = Guid.NewGuid();
        }
                 
        public Guid Id { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UrlString { get; set; }
        public DateTime OperationDate { get; set; }
        public string Description { get; set; }
        public int PlayNumber { get; set; }

        public Genre Genre { get; set; }
        public Artist Artist { get; set; }

    }
}