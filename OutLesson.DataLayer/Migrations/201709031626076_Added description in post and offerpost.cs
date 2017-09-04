namespace OutLesson.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddescriptioninpostandofferpost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferPosts", "Description", c => c.String());
            AddColumn("dbo.Posts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Description");
            DropColumn("dbo.OfferPosts", "Description");
        }
    }
}
