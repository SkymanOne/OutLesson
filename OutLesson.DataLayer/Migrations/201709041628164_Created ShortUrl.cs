namespace OutLesson.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedShortUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferPosts", "ShortUrl", c => c.String());
            AddColumn("dbo.Posts", "ShortUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ShortUrl");
            DropColumn("dbo.OfferPosts", "ShortUrl");
        }
    }
}
