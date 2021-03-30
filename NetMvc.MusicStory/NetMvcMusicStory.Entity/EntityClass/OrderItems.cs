using NetMvcMusicStory.Interface.Infrastructure;
using System;

namespace NetMvcMusicStory.Entity
{
    /// <summary>
    /// 订单详情业务实体
    /// </summary>
    public class OrderItems : IEntity
    {
        public OrderItems()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string OrderNember { get; set; }
        public string Name { get; set; }
        public DateTime OrderTime { get; set; }//创建时间
        public DateTime OperationDate { get; set; }//操作时间
        public int? Is_delete { get; set; }//订单是否删除
        public string PayMethod { get; set; }
        public Guid ShoppingCartItemsId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubPrice { get; set; }
        public Guid UserId { get; set; }
        public ShoppingCartItems ShoppingCartItems { get; set; }
    }
}
