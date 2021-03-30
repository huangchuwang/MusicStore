using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{

    /// <summary>
    /// 购物车商品业务实体
    /// </summary>
    public class ShoppingCartItems : IEntity
    {
        public ShoppingCartItems()
        {
            this.Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotalPrice { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Guid AlbumId { get; set; }

        public int? Statu { get; set; }//1为提交，0为未提交
        public ShoppingCart ShoppingCart { get; set; }
        public Album Album { get; set; }
    }
}
