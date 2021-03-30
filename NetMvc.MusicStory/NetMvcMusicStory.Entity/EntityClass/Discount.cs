using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetMvcMusicStory.Interface.Infrastructure;

namespace NetMvcMusicStory.Entity.EntityClass
{
    public class Discount : IEntity
    {
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public DateTime StartTime { get; set; }//开始时间
        public DateTime EndTime { get; set; }//结束时间
        public decimal AlbumDiscount { get; set; }
        public int Statu { get; set; }//状态，是否过期   1是有效 0为过期
    }
}