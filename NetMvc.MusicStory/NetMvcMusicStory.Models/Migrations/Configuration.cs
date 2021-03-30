namespace NetMvcMusicStory.Models.Migrations
{
    using Entity;
    using MusicStoryDbConnection;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NetMvcMusicStory.Models.MusicStoryDbConnection.MusicStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NetMvcMusicStory.Models.MusicStoryDbConnection.MusicStoreDbContext context)
        {
            //建库完后额外更新4次，轮番注释
            //AddGenre(context); AddArtist(context); AddRoles(context);
            //AddRules(context); AddUser(context);
            //AddShoppingCart(context); AddAlbum(context);
            //AddMusic(context); AddPraise(context); AddComment(context);  //有错误，待修改 AddShoppingCartItems(context);
        }

        /// <summary>
        /// 添加歌手
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddArtist(MusicStoreDbContext _dbContext)
        {
            var aCollection = new List<Artist>()
            {
                new Artist() {Name="薛之谦",Description="薛之谦（Joker Xue），1983年7月17日出生于上海，中国内地流行乐男歌手、影视演员、音乐制作人，毕业于格里昂酒店管理学院。" },
                new Artist() {Name="邓紫棋",Description="邓紫棋（Gloria Tang Tsz-Kei），本名邓诗颖，1991年8月16日生于中国上海，4岁移居香港，中国香港女歌手、词曲创作人、音乐制作人。" },
                new Artist() {Name="林俊杰",Description="林俊杰（JJ Lin），1981年3月27日出生于新加坡，祖籍中国福建省厦门市同安区，华语流行乐男歌手、作曲人、音乐制作人、潮牌主理人。" },
                new Artist() {Name="周杰伦",Description="周杰伦（Jay Chou），1979年1月18日出生于台湾省新北市，祖籍福建省泉州市永春县，中国台湾流行乐男歌手、原创音乐人、演员、导演、编剧，毕业于淡江中学。" },
                new Artist() {Name="陈奕迅",Description="陈奕迅（Eason Chan），1974年7月27日出生于中国香港，祖籍广东省东莞市，华语流行乐男歌手、演员、作曲人，毕业于英国金斯顿大学。" },
                new Artist() {Name="李荣浩",Description="李荣浩（Ronghao Li），1985年7月11日出生于安徽省蚌埠市，中国内地流行乐男歌手、词曲创作人、音乐制作人、演员。" },
                new Artist() {Name="华晨宇",Description="华晨宇，1990年2月7日生于湖北省十堰市，中国内地男歌手、作曲人，毕业于武汉音乐学院。" },
                new Artist() {Name="郑贵添",Description="郑贵添，2001年10月13日生于广西省合浦县，国台湾流行乐男歌手、原创音乐人、演员、导演、编剧，毕业于柳州职业技术学院软件技术专业。" },
                new Artist() {Name="黄初旺",Description="黄初旺，2020年12月27日从柳州职业技术学院D区运动场一块石头里蹦出来，华语流行乐男歌手、演员、作曲人，毕业于柳州职业技术学院软件技术专业。" }
            };
            aCollection.ForEach(x => _dbContext.Artist.AddOrUpdate(x));
        }

        /// <summary>
        /// 添加流派
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddGenre(MusicStoreDbContext _dbContext)
        {
            var gCollection = new List<Genre>()
            {
                new Genre() {Name="流行",Description="流行音乐19世纪末20世纪初起源于美国，从音乐体系看，流行音乐是在叮砰巷歌曲、布鲁斯、爵士乐、摇滚乐、索尔音乐等美国大众音乐架构基础上发展起来的音乐。其风格多样，形态丰富，可泛指Jazz、Rock、Soul、Blues、Reggae、Rap、Hip-Hop、Disco、New Age、Funk、R&B等20世纪后诞生的都市化大众商品音乐。" },
                new Genre() {Name="古典",Description="古典音乐 ，是音乐的一种类型。古典音乐有广义、狭义之分。广义是指那些从西方中世纪开始至今的、在欧洲主流文化背景下创作的西方古典音乐，主要因其复杂多样的创作技术和所能承载的厚重内涵而有别于通俗音乐和民间音乐。狭义指古典主义时期，1750年（J·S·巴赫去世）至1827年（贝多芬去世)，这一时期为古典主义音乐时期，它包含了两大时间段：“前古典时期”和“维也纳古典时期”。“最为著名的维也纳乐派也是在“维也纳古典时期”兴起，其代表作曲家有海顿、莫扎特和贝多芬，被后世称为“维也纳三杰”。" },
                new Genre() {Name="摇滚",Description="摇滚乐，英文全称为Rock and Roll，兴起于20世纪50年代中期，主要受到节奏布鲁斯、乡村音乐和叮砰巷音乐的影响发展而来。早期摇滚乐很多都是黑人节奏布鲁斯的翻唱版，因而节奏布鲁斯是其主要根基。摇滚乐分支众多，形态复杂，主要风格有：民谣摇滚、艺术摇滚、迷幻摇滚、乡村摇滚、重金属、朋克、另类摇滚等，代表人物有：埃尔维斯·普莱斯利（猫王）、鲍勃·迪伦、披头士乐队、滚石乐队、地下丝绒乐队、黑色安息日乐队等，是20世纪美国大众音乐走向成熟的重要标志。" },
                new Genre() {Name="R&B",Description="节奏布鲁斯（英语：Rhythm and Blues，简称：R&B或RnB，台湾、港澳、马新作节奏蓝调 ），又称节奏怨曲。是一种首先非裔美国人艺术家所演奏，并融合了爵士乐、福音音乐和电子布鲁斯音乐的音乐形式。这个音乐术语由是美国告示牌（Billboard）于1940年代末所提出。" },
                new Genre() {Name="蓝调",Description="蓝调（Blues）为摇滚及福音歌曲（Gospel）的老祖宗，原本只是美国早期黑奴抒发心情时所吟唱的12小节曲式，演唱或演奏时大量蓝调音（Blue Notes）的应用，使得音乐上充满了压抑及不和谐的感觉，这种音乐听起来十分忧郁(Blue)。但就是这么一股“反骨”气息，使得它后来在叛逆的摇滚乐中发扬光大。蓝调以歌曲直接陈述内心想法的表现方式，与当时白人社会的音乐截然不同。" },
                new Genre() {Name="轻音乐",Description="轻音乐以通俗方式诠释乐曲，其来源可以是原创，也可以是对古典音乐、流行音乐或者民间音乐进行改编而成。轻音乐一般以小型乐队加以演奏，结构简单、节奏明快、旋律优美。轻音乐可以营造温馨浪漫的情调，带有休闲性质。轻音乐起源于一战后的英国，在20世纪中后期达到鼎盛，在二十世纪末渐被新纪元音乐取代，但时至今日尚有一定影响力。" }
            };
            gCollection.ForEach(x => _dbContext.Genre.AddOrUpdate(x));
        }

        /// <summary>
        /// 添加专辑
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddAlbum(MusicStoreDbContext _dbContext)
        {
            var g1 = _dbContext.Genre.Where(x => x.Name == "流行").Select(c => c.Id).FirstOrDefault();
            var g2 = _dbContext.Genre.Where(x => x.Name == "古典").Select(c => c.Id).FirstOrDefault();
            var g3 = _dbContext.Genre.Where(x => x.Name == "摇滚").Select(c => c.Id).FirstOrDefault();
            var a1 = _dbContext.Artist.Where(x => x.Name == "林俊杰").Select(c => c.Id).FirstOrDefault();
            var a2 = _dbContext.Artist.Where(x => x.Name == "薛之谦").Select(c => c.Id).FirstOrDefault();
            var a3 = _dbContext.Artist.Where(x => x.Name == "周杰伦").Select(c => c.Id).FirstOrDefault();
            var a4 = _dbContext.Artist.Where(x => x.Name == "华晨宇").Select(c => c.Id).FirstOrDefault();
            var a5 = _dbContext.Artist.Where(x => x.Name == "李荣浩").Select(c => c.Id).FirstOrDefault();

            var gCollection = new List<Album>()
            {
                new Album() {Name="尘",Description="薛之谦 2019 全新专辑\n尘无名  木偶心 十指缝间十页事\n如果不是有些名字最后还是渐行渐远漂浮风中\n怎么知道许多往日终将无依无靠坠落如尘\n木偶看着多少爱情在安静来去\n不笑 不哭...", OperationDate=DateTime.Now,Price=30.00M,GenreId=g1,ArtistId=a2,UrlString="../Content/UploaFile/Photo/c.jpg" ,PlayNumber=140328 },
                new Album() {Name="幸存者 Drifter",Description="如果你 不只是你，那会怎样？\nJJ林俊杰 第14张大碟 幸存者•如你\n双维度EP 创造全新音乐视角\n由JJ 林俊杰 亲自领导整张专辑的企划创意与视觉\n双EP的两条故事线是并行也是交叠\nJJ林...", OperationDate=DateTime.Now,Price=50.00M,GenreId=g2,ArtistId=a1,UrlString="../Content/UploaFile/Photo/xcz.jpg" ,PlayNumber=12198},
                new Album() {Name="等你下课",Description="周杰伦 2018最新单曲\n「等你下课」\n不想让你等太久！\n周杰伦给歌迷的惊喜\n特选1/18 生日当天发布\n文青疗愈系暗恋情歌「等你下课」\n努力考上跟你喜欢的人一样的学校\n顺理成章「等你下课」！...\n",Price=20.00M, OperationDate=DateTime.Now,GenreId=g3,ArtistId=a3,UrlString="../Content/UploaFile/Photo/dnxk.jpg" ,PlayNumber=148201},
                new Album() {Name="我管你 (真我版)",Description="不想被说教\n也不必被定义\n我的世界，由我表达\n华晨宇开启新摇滚时代\n释放中国真我音乐新力量！\n华晨宇\n《我管你》-真我版\n百名歌迷齐录制和声 华晨宇“活出真我”打造自己的摇滚时代\n华晨宇...",Price=15.00M, OperationDate=DateTime.Now,GenreId=g1,ArtistId=a4,UrlString="../Content/UploaFile/Photo/wgn.jpg" ,PlayNumber=119553},
                new Album() {Name="有理想",Description="在浮夸世界里的低调坚实\n用他的音乐 拆解揭露我们认知的现实\n所谓理想 不过平凡真实\n就算曾经迷茫蹉跎 也是一种琢磨\n『亚洲新天王』李荣浩\n2016全新创作大碟【有理想】\n在你我日夜生活 翻来覆去的...",Price=80.00M, OperationDate=DateTime.Now,GenreId=g1,ArtistId=a5,UrlString="../Content/UploaFile/Photo/ylx.jpg" ,PlayNumber=130071}
            };
            gCollection.ForEach(x => _dbContext.Album.AddOrUpdate(x));
        }

        /// <summary>
        /// 添加音乐
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddMusic(MusicStoreDbContext _dbContext)
        {
            var m1 = _dbContext.Album.Where(x => x.Name == "尘").Select(c => c.Id).FirstOrDefault();
            var m2 = _dbContext.Album.Where(x => x.Name == "幸存者 Drifter").Select(c => c.Id).FirstOrDefault();
            var m3 = _dbContext.Album.Where(x => x.Name == "等你下课").Select(c => c.Id).FirstOrDefault();
            var m4 = _dbContext.Album.Where(x => x.Name == "我管你 (真我版)").Select(c => c.Id).FirstOrDefault();
            var m5 = _dbContext.Album.Where(x => x.Name == "有理想").Select(c => c.Id).FirstOrDefault();

            var gCollection = new List<Music>()
            {
                //《尘》专辑-薛之谦
                new Music() {Name="尘",Description="这样看来 是我不该", MusicUrl="../Content/UploaFile/Music/c.mp3",Lrc="../Content/UploaFile/Lrc/c.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="病态",Description="直到 病态的时代无力负载", MusicUrl="../Content/UploaFile/Music/bt.mp3",Lrc="../Content/UploaFile/Lrc/bt.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="环",Description="无限循环", MusicUrl="../Content/UploaFile/Music/h.mp3",Lrc="../Content/UploaFile/Lrc/h.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="慢半拍",Description="对过往的自己 敬个礼", MusicUrl="../Content/UploaFile/Music/mbp.mp3",Lrc="../Content/UploaFile/Lrc/mbp.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="木偶人",Description="防备厚厚一本", MusicUrl="../Content/UploaFile/Music/mor.mp3",Lrc="../Content/UploaFile/Lrc/mor.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="陪你去流浪",Description="我看着湖面 平平 淡淡", MusicUrl="../Content/UploaFile/Music/pnqll.mp3",Lrc="../Content/UploaFile/Lrc/pnqll.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="配合",Description="口诛笔伐 请你别停下", MusicUrl="../Content/UploaFile/Music/ph.mp3",Lrc="../Content/UploaFile/Lrc/ph.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="笑场",Description="如同我脸上画了滑稽的妆", MusicUrl="../Content/UploaFile/Music/xc.mp3",Lrc="../Content/UploaFile/Lrc/xc.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                new Music() {Name="这么久没见",Description="先学沉默寡言 再学满嘴谎言", MusicUrl="../Content/UploaFile/Music/zmjmj.mp3",Lrc="../Content/UploaFile/Lrc/zmjmj.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m1 },
                
                //《幸存者》专辑-林俊杰
                new Music() {Name="幸存者",Description="背着梦的幸存者", MusicUrl="../Content/UploaFile/Music/xcz.mp3",Lrc="../Content/UploaFile/Lrc/xcz.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m2 },
                new Music() {Name="最向往的地方",Description="穿越人海和时间的浪", MusicUrl="../Content/UploaFile/Music/zxwddf.mp3",Lrc="../Content/UploaFile/Lrc/zxwddf.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m2 },
                new Music() {Name="最好是",Description="穿越人海和时间的浪", MusicUrl="../Content/UploaFile/Music/zhs.mp3",Lrc="../Content/UploaFile/Lrc/zhs.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m2 },
                new Music() {Name="离开的那一些",Description="原来放手也是一种美一种的了解", MusicUrl="../Content/UploaFile/Music/lkdnyx.mp3",Lrc="../Content/UploaFile/Lrc/lkdnyx.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m2 },
                new Music() {Name="暂时的记号",Description="提醒自己更善良", MusicUrl="../Content/UploaFile/Music/zsdjh.mp3",Lrc="../Content/UploaFile/Lrc/zsdjh.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m2 },

                //《我管你 (真我版)》专辑-华晨宇
                new Music() {Name="我管你 (真我版)",Description="我的世界我来浮夸", MusicUrl="../Content/UploaFile/Music/wgn.mp3",Lrc="../Content/UploaFile/Lrc/wgn.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m4 },

                //《有理想》专辑-李荣浩
                new Music() {Name="野生动物",Description="倒不如做野生动物", MusicUrl="../Content/UploaFile/Music/ysdw.mp3",Lrc="../Content/UploaFile/Lrc/ysdw.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="满座",Description="闭着眼的床很暖和被几首歌勒索", MusicUrl="../Content/UploaFile/Music/mz.mp3",Lrc="../Content/UploaFile/Lrc/mz.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="有理想",Description="其实都算是有理想", MusicUrl="../Content/UploaFile/Music/ylx.mp3",Lrc="../Content/UploaFile/Lrc/ylx.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="爸爸妈妈",Description="爸爸妈妈给我的不少不多", MusicUrl="../Content/UploaFile/Music/bbmm.mp3",Lrc="../Content/UploaFile/Lrc/bbmm.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="流行歌曲",Description="人人都有几句流行歌曲", MusicUrl="../Content/UploaFile/Music/lxgq.mp3",Lrc="../Content/UploaFile/Lrc/lxgq.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="优点",Description="要么刻苦要么铭心", MusicUrl="../Content/UploaFile/Music/yd.mp3",Lrc="../Content/UploaFile/Lrc/yd.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="不将就",Description="算一算虚度了多少个年头", MusicUrl="../Content/UploaFile/Music/bjj.mp3",Lrc="../Content/UploaFile/Lrc/bjj.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="心里面",Description="住的地点下起了雪", MusicUrl="../Content/UploaFile/Music/xlm.mp3",Lrc="../Content/UploaFile/Lrc/xlm.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="女孩",Description="幸福吗都说不知道", MusicUrl="../Content/UploaFile/Music/nh.mp3",Lrc="../Content/UploaFile/Lrc/nh.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="宛若新生",Description="一个人会太无聊，两个人又太奇妙", MusicUrl="../Content/UploaFile/Music/wrxs.mp3",Lrc="../Content/UploaFile/Lrc/wrxs.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 },
                new Music() {Name="大太阳",Description="照常那样", MusicUrl="../Content/UploaFile/Music/dty.mp3",Lrc="../Content/UploaFile/Lrc/dty.lrc",Create_time=DateTime.Now,Update_time=DateTime.Now, AlbumId=m5 }
            };
            gCollection.ForEach(x => _dbContext.Musics.AddOrUpdate(x));
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddComment(MusicStoreDbContext _dbContext)
        {
            var m1 = _dbContext.Album.Where(x => x.Name == "尘").Select(c => c.Id).FirstOrDefault();
            var m2 = _dbContext.Album.Where(x => x.Name == "幸存者 Drifter").Select(c => c.Id).FirstOrDefault();
            var m3 = _dbContext.Album.Where(x => x.Name == "有理想").Select(c => c.Id).FirstOrDefault();
            var m4 = _dbContext.Album.Where(x => x.Name == "我管你 (真我版)").Select(c => c.Id).FirstOrDefault();
            var u1 = _dbContext.Users.Where(x => x.Name == "郑贵添").Select(c => c.Id).FirstOrDefault();
            var u2 = _dbContext.Users.Where(x => x.Name == "旺添cp我磕爆").Select(c => c.Id).FirstOrDefault();
            var u3 = _dbContext.Users.Where(x => x.Name == "黄初旺").Select(c => c.Id).FirstOrDefault();
            var u4 = _dbContext.Users.Where(x => x.Name == "毅桑").Select(c => c.Id).FirstOrDefault();

            var gCollection = new List<Comment>()
            {
                new Comment() { Id=Guid.NewGuid(), Content="代入感很强，我已经是郑之谦了！" ,AlbumId=m1, UserId=u1, Create_time=Convert.ToDateTime("2020/12/31 17:19"), Comment_State=1, Star=3.5 },
                new Comment() { Id=Guid.NewGuid(), Content="唱的还没我好，这也能出专辑？" ,AlbumId=m2, UserId=u1, Create_time=Convert.ToDateTime("2019/10/31 9:40"), Comment_State=1, Star=3.8 },
                new Comment() { Id=Guid.NewGuid(), Content="李荣浩啥都好，就是唱歌不尊重人，不睁眼睛。" ,AlbumId=m3, UserId=u1, Create_time=Convert.ToDateTime("2020/06/14 21:09"), Comment_State=1, Star=4.1 },
                new Comment() { Id=Guid.NewGuid(), Content="哦哦哦哦哦哦哦哦哦~" ,AlbumId=m4, UserId=u1, Create_time=Convert.ToDateTime("2018/08/22 14:19"), Comment_State=1, Star=5 },
                new Comment() { Id=Guid.NewGuid(), Content="郑贵添永远love黄初旺！" ,AlbumId=m1, UserId=u2, Create_time=DateTime.Now, Comment_State=1, Star=5 },
                new Comment() { Id=Guid.NewGuid(), Content="郑贵添黄初旺在永远在一起！！" ,AlbumId=m2, UserId=u2, Create_time=Convert.ToDateTime("2020/11/11 11:11"), Comment_State=1, Star=4.5 },
                new Comment() { Id=Guid.NewGuid(), Content="郑贵添黄初旺一胎九仔！" ,AlbumId=m3, UserId=u2, Create_time=Convert.ToDateTime("2020/09/18 7:47"), Comment_State=1, Star=3 },
                new Comment() { Id=Guid.NewGuid(), Content="新年快乐！~" ,AlbumId=m4, UserId=u2, Create_time=Convert.ToDateTime("2021/1/1 00:01"), Comment_State=1, Star=3.5 },
                new Comment() { Id=Guid.NewGuid(), Content="挺一般的我觉得！" ,AlbumId=m1, UserId=u3, Create_time=Convert.ToDateTime("2020/12/29 12:32"), Comment_State=1, Star=4.8 },
                new Comment() { Id=Guid.NewGuid(), Content="JJ唱功真是越来越好了" ,AlbumId=m2, UserId=u3, Create_time=Convert.ToDateTime("2020/12/30 18:19"), Comment_State=1, Star=5 },
                new Comment() { Id=Guid.NewGuid(), Content="别整天人身攻击好吗，你能决定眼睛大小的吗" ,AlbumId=m3, UserId=u3, Create_time=Convert.ToDateTime("2020/06/14 21:38"), Comment_State=1, Star=3 },
                new Comment() { Id=Guid.NewGuid(), Content="哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦~" ,AlbumId=m4, UserId=u3, Create_time=Convert.ToDateTime("2019/08/22 14:19"), Comment_State=1, Star=4.2 },
                new Comment() { Id=Guid.NewGuid(), Content="本当に素晴らしい歌手です" ,AlbumId=m1, UserId=u4, Create_time=Convert.ToDateTime("2020/05/20 17:20"), Comment_State=1, Star=3.5 },
                new Comment() { Id=Guid.NewGuid(), Content="夢中で聞いています。" ,AlbumId=m2, UserId=u4, Create_time=Convert.ToDateTime("2020/02/27 17:55"), Comment_State=1, Star=3.5 },
                new Comment() { Id=Guid.NewGuid(), Content="2020年的今天会有人说他眼睛小！" ,AlbumId=m3, UserId=u4, Create_time=Convert.ToDateTime("2019/06/14 10:17"), Comment_State=1, Star=5 },
                new Comment() { Id=Guid.NewGuid(), Content="哦哦哦哦哦哦哦哦哦哦哦哦哦哦~" ,AlbumId=m4, UserId=u4, Create_time=Convert.ToDateTime("2020/08/22 14:19"), Comment_State=1, Star=4.4 },

            };
            gCollection.ForEach(x => _dbContext.Comments.AddOrUpdate(x));
        }

        /// <summary>
        /// 添加点赞
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddPraise(MusicStoreDbContext _dbContext)
        {
            var m1 = _dbContext.Album.Where(x => x.Name == "尘").Select(c => c.Id).FirstOrDefault();
            var m2 = _dbContext.Album.Where(x => x.Name == "幸存者 Drifter").Select(c => c.Id).FirstOrDefault();
            var m3 = _dbContext.Album.Where(x => x.Name == "有理想").Select(c => c.Id).FirstOrDefault();
            var u1 = _dbContext.Users.Where(x => x.Name == "郑贵添").Select(c => c.Id).FirstOrDefault();
            var u2 = _dbContext.Users.Where(x => x.Name == "黄初旺").Select(c => c.Id).FirstOrDefault();

            var gCollection = new List<Praise>()
            {
                new Praise() { Id=Guid.NewGuid(), AlbumId=m1, UserId=u1, Update_time=DateTime.Now, Praise_State=1 },
                new Praise() { Id=Guid.NewGuid(), AlbumId=m1, UserId=u2, Update_time=DateTime.Now, Praise_State=1 },
                new Praise() { Id=Guid.NewGuid(), AlbumId=m2, UserId=u1, Update_time=DateTime.Now, Praise_State=1 },
                new Praise() { Id=Guid.NewGuid(), AlbumId=m3, UserId=u1, Update_time=DateTime.Now, Praise_State=1 },
                new Praise() { Id=Guid.NewGuid(), AlbumId=m3, UserId=u2, Update_time=DateTime.Now, Praise_State=1 }

            };
            gCollection.ForEach(x => _dbContext.Praises.AddOrUpdate(x));
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddUser(MusicStoreDbContext _dbContext)
        {
            var r1 = _dbContext.Role.Where(x => x.Title == "普通用户").Select(c => c.Id).FirstOrDefault();
            var r2 = _dbContext.Role.Where(x => x.Title == "超级管理员").Select(c => c.Id).FirstOrDefault();
            var gCollection = new List<User>()
            {
                //账号Admin 密码123456
                new User() {Create_time=DateTime.Now,  Id=Guid.NewGuid(), Name="黄初旺" ,Account="e3afed0047b08059d0fada10f400c1e5",Password="e10adc3949ba59abbe56e057f20f883e",Email="",Icon="../Content/UploaFile/UserIcon/hcw.jpg",RoleId=r2,Is_delete=0,Status=1},
                //账号123123 密码123456
                new User() {Create_time=DateTime.Now,  Id=Guid.NewGuid(), Name="毅桑" ,Account="4297f44b13955235245b2497399d7a93",Password="e10adc3949ba59abbe56e057f20f883e",Email="",Icon="../Content/UploaFile/UserIcon/ys.jpg",RoleId=r1,Is_delete=0,Status=1},
                //账号654321 密码123456
                new User() {Create_time=DateTime.Now,  Id=Guid.NewGuid(), Name="郑贵添" ,Account="c33367701511b4f6020ec61ded352059",Password="e10adc3949ba59abbe56e057f20f883e",Email="",Icon="../Content/UploaFile/UserIcon/ys.jpg",RoleId=r1,Is_delete=0,Status=1},
                //账号123456 密码123456
                new User() {Create_time=DateTime.Now,  Id=Guid.NewGuid(), Name="旺添cp我磕爆" ,Account="e10adc3949ba59abbe56e057f20f883e",Password="e10adc3949ba59abbe56e057f20f883e",Email="",Icon="../Content/UploaFile/UserIcon/ys.jpg",RoleId=r1,Is_delete=0,Status=1},
            };
            gCollection.ForEach(x => _dbContext.Users.AddOrUpdate(x));
        }


        /// <summary>
        /// 种子角色
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddRoles(MusicStoreDbContext _dbContext)
        {
            var gCollection = new List<Role>()
            {
                new Role() { Id=Guid.NewGuid(), Title="超级管理员",Status=1,Create_time=DateTime.Now,Update_time=DateTime.Now },
                new Role() { Id=Guid.NewGuid(), Title="管理员",Status=1,Create_time=DateTime.Now,Update_time=DateTime.Now },
                new Role() { Id=Guid.NewGuid(), Title="普通用户",Status=1,Create_time=DateTime.Now,Update_time=DateTime.Now },
            };
            gCollection.ForEach(x => _dbContext.Role.AddOrUpdate(x));
        }


        /// <summary>
        /// 权限规则
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddRules(MusicStoreDbContext _dbContext)
        {
            var r1 = _dbContext.Role.Where(x => x.Title == "超级管理员").Select(c => c.Id).FirstOrDefault();
            var r2 = _dbContext.Role.Where(x => x.Title == "普通用户").Select(c => c.Id).FirstOrDefault();
            var gCollection = new List<Rule>()
            {
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/RuleManagement/Index", Status=1,Title="规则管理",Create_time=DateTime.Now,Update_time=DateTime.Now,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/RoleManagemen/Index",Status=1, Title="角色管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/UserManagemen/Index",Status=1, Title="用户管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AdminGenre/Index",Status=1, Title="流派管理",Create_time=DateTime.Now,Update_time=DateTime.Now,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AdminArtist/Index",Status=1, Title="歌手管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AdminAlbum/Index",Status=1, Title="专辑管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AdminMusic/Index",Status=1, Title="音乐管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/ShoppingCartManage/Index",Status=1, Title="购物车管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AccessControl/Index",Status=1, Title="用户访问控制",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="/AdminOderItems/Index",Status=1, Title="订单管理",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r1 },

                new Rule() { Id=Guid.NewGuid(),MenuUrl="Home",Status=1, Title="首页",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r2 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="PlayMusic",Status=1, Title="音乐播放",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r2 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="ShoppingCart",Status=1, Title="购物车",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r2 },
                new Rule() { Id=Guid.NewGuid(),MenuUrl="ShoppingCommit",Status=1, Title="订单提交",Create_time=DateTime.Now,Update_time=DateTime.Now ,RoleId=r2 },
            }; 
            gCollection.ForEach(x => _dbContext.Rule.AddOrUpdate(x));
        }


        /// <summary>
        /// 普通用户购物车初始化
        /// </summary>
        /// <param name="_dbContext"></param>
        public static void AddShoppingCart(MusicStoreDbContext _dbContext)
        {
            var r1 = _dbContext.Users.Where(x => x.Role.Title == "普通用户").Select(c => c.Id).FirstOrDefault();
            var gCollection = new List<ShoppingCart>()
            {
                new ShoppingCart() { Id=Guid.NewGuid(), Count=1,UserId=r1 },
            };
            gCollection.ForEach(x => _dbContext.ShoppingCart.AddOrUpdate(x));
        }

    }
}
