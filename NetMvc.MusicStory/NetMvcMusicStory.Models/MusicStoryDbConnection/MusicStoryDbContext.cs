using NetMvcMusicStory.Entity;
using NetMvcMusicStory.Entity.EntityClass;
using System.Data.Entity;

namespace NetMvcMusicStory.Models.MusicStoryDbConnection
{
    //业务实体
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext() : base("Server=localhost;Initial Catalog=NetMvcMusicStory;uid=sa;pwd=123456;MultipleActiveResultSets=true") { }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Praise> Praises { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Rule> Rule { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}