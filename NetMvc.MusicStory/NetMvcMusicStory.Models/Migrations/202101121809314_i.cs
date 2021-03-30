namespace NetMvcMusicStory.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class i : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GenreId = c.Guid(nullable: false),
                        ArtistId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UrlString = c.String(),
                        OperationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        PlayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        AlbumId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Create_time = c.DateTime(nullable: false),
                        Comment_State = c.Int(nullable: false),
                        Star = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Account = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Icon = c.String(),
                        Status = c.Int(),
                        Is_delete = c.Int(),
                        RoleId = c.Guid(),
                        Create_time = c.DateTime(),
                        Update_time = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Status = c.Int(nullable: false),
                        Create_time = c.DateTime(nullable: false),
                        Update_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AlbumId = c.Guid(nullable: false),
                        Name = c.String(),
                        MusicUrl = c.String(),
                        Lrc = c.String(),
                        Description = c.String(),
                        Create_time = c.DateTime(nullable: false),
                        Update_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNember = c.String(),
                        Name = c.String(),
                        OrderTime = c.DateTime(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        Is_delete = c.Int(),
                        PayMethod = c.String(),
                        ShoppingCartItemsId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCartItems", t => t.ShoppingCartItemsId, cascadeDelete: true)
                .Index(t => t.ShoppingCartItemsId);
            
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShoppingCartId = c.Guid(nullable: false),
                        AlbumId = c.Guid(nullable: false),
                        Statu = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.ShoppingCartId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Praises",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AlbumId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Update_time = c.DateTime(nullable: false),
                        Praise_State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Status = c.Int(nullable: false),
                        Front = c.String(),
                        MenuUrl = c.String(),
                        RoleId = c.Guid(nullable: false),
                        Create_time = c.DateTime(nullable: false),
                        Update_time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rules", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.OrderItems", "ShoppingCartItemsId", "dbo.ShoppingCartItems");
            DropForeignKey("dbo.ShoppingCarts", "UserId", "dbo.Users");
            DropForeignKey("dbo.ShoppingCartItems", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCartItems", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Musics", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Rules", new[] { "RoleId" });
            DropIndex("dbo.ShoppingCarts", new[] { "UserId" });
            DropIndex("dbo.ShoppingCartItems", new[] { "AlbumId" });
            DropIndex("dbo.ShoppingCartItems", new[] { "ShoppingCartId" });
            DropIndex("dbo.OrderItems", new[] { "ShoppingCartItemsId" });
            DropIndex("dbo.Musics", new[] { "AlbumId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropTable("dbo.Rules");
            DropTable("dbo.Praises");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.ShoppingCartItems");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Musics");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Genres");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
