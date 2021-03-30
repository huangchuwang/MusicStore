using NetMvcMusicStory.Interface.Infrastructure;
using System;
using System.Collections.Generic;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 购物车业务实体
    /// </summary>
    public class ShoppingCart : IEntity
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }
        public User User { get; set; }
        public List<ShoppingCartItems> ShoppingCartItems { get; set; }
    }
}