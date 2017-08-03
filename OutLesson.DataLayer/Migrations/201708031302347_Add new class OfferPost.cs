namespace OutLesson.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewclassOfferPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfferPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Time = c.DateTime(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        Autor_Id = c.String(maxLength: 128),
                        WhoAcceptedPublish_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Autor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.WhoAcceptedPublish_Id)
                .Index(t => t.Autor_Id)
                .Index(t => t.WhoAcceptedPublish_Id);
            
            AddColumn("dbo.Tags", "OfferPost_Id", c => c.Int());
            CreateIndex("dbo.Tags", "OfferPost_Id");
            AddForeignKey("dbo.Tags", "OfferPost_Id", "dbo.OfferPosts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OfferPosts", "WhoAcceptedPublish_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "OfferPost_Id", "dbo.OfferPosts");
            DropForeignKey("dbo.OfferPosts", "Autor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "OfferPost_Id" });
            DropIndex("dbo.OfferPosts", new[] { "WhoAcceptedPublish_Id" });
            DropIndex("dbo.OfferPosts", new[] { "Autor_Id" });
            DropColumn("dbo.Tags", "OfferPost_Id");
            DropTable("dbo.OfferPosts");
        }
    }
}
