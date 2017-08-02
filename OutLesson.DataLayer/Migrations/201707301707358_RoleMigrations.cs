using System.Data.Entity.Migrations;

namespace OutLesson.DataLayer.Migrations
{
	public partial class RoleMigrations : DbMigration
	{
		public override void Up()
		{
			CreateTable(
					"dbo.Posts",
					c => new
					{
						Id = c.Int(false, true),
						Title = c.String(),
						Content = c.String(),
						Time = c.DateTime(false),
						Autor_Id = c.String(maxLength: 128)
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.AspNetUsers", t => t.Autor_Id)
				.Index(t => t.Autor_Id);

			CreateTable(
					"dbo.AspNetUsers",
					c => new
					{
						Id = c.String(false, 128),
						Year = c.Int(false),
						FullName = c.String(),
						Email = c.String(maxLength: 256),
						EmailConfirmed = c.Boolean(false),
						PasswordHash = c.String(),
						SecurityStamp = c.String(),
						PhoneNumber = c.String(),
						PhoneNumberConfirmed = c.Boolean(false),
						TwoFactorEnabled = c.Boolean(false),
						LockoutEndDateUtc = c.DateTime(),
						LockoutEnabled = c.Boolean(false),
						AccessFailedCount = c.Int(false),
						UserName = c.String(false, 256),
						Tag_Id = c.Int()
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Tags", t => t.Tag_Id)
				.Index(t => t.UserName, unique: true, name: "UserNameIndex")
				.Index(t => t.Tag_Id);

			CreateTable(
					"dbo.AspNetUserClaims",
					c => new
					{
						Id = c.Int(false, true),
						UserId = c.String(false, 128),
						ClaimType = c.String(),
						ClaimValue = c.String()
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
				.Index(t => t.UserId);

			CreateTable(
					"dbo.AspNetUserLogins",
					c => new
					{
						LoginProvider = c.String(false, 128),
						ProviderKey = c.String(false, 128),
						UserId = c.String(false, 128)
					})
				.PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
				.Index(t => t.UserId);

			CreateTable(
					"dbo.AspNetUserRoles",
					c => new
					{
						UserId = c.String(false, 128),
						RoleId = c.String(false, 128)
					})
				.PrimaryKey(t => new {t.UserId, t.RoleId})
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
				.ForeignKey("dbo.AspNetRoles", t => t.RoleId, true)
				.Index(t => t.UserId)
				.Index(t => t.RoleId);

			CreateTable(
					"dbo.Tags",
					c => new
					{
						Id = c.Int(false, true),
						Name = c.String(),
						Description = c.String()
					})
				.PrimaryKey(t => t.Id);

			CreateTable(
					"dbo.AspNetRoles",
					c => new
					{
						Id = c.String(false, 128),
						Name = c.String(false, 256),
						Description = c.String(),
						Discriminator = c.String(false, 128)
					})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Name, unique: true, name: "RoleNameIndex");

			CreateTable(
					"dbo.TagPosts",
					c => new
					{
						Tag_Id = c.Int(false),
						Post_Id = c.Int(false)
					})
				.PrimaryKey(t => new {t.Tag_Id, t.Post_Id})
				.ForeignKey("dbo.Tags", t => t.Tag_Id, true)
				.ForeignKey("dbo.Posts", t => t.Post_Id, true)
				.Index(t => t.Tag_Id)
				.Index(t => t.Post_Id);
		}

		public override void Down()
		{
			DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
			DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
			DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
			DropForeignKey("dbo.AspNetUsers", "Tag_Id", "dbo.Tags");
			DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.Posts", "Autor_Id", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
			DropIndex("dbo.TagPosts", new[] {"Post_Id"});
			DropIndex("dbo.TagPosts", new[] {"Tag_Id"});
			DropIndex("dbo.AspNetRoles", "RoleNameIndex");
			DropIndex("dbo.AspNetUserRoles", new[] {"RoleId"});
			DropIndex("dbo.AspNetUserRoles", new[] {"UserId"});
			DropIndex("dbo.AspNetUserLogins", new[] {"UserId"});
			DropIndex("dbo.AspNetUserClaims", new[] {"UserId"});
			DropIndex("dbo.AspNetUsers", new[] {"Tag_Id"});
			DropIndex("dbo.AspNetUsers", "UserNameIndex");
			DropIndex("dbo.Posts", new[] {"Autor_Id"});
			DropTable("dbo.TagPosts");
			DropTable("dbo.AspNetRoles");
			DropTable("dbo.Tags");
			DropTable("dbo.AspNetUserRoles");
			DropTable("dbo.AspNetUserLogins");
			DropTable("dbo.AspNetUserClaims");
			DropTable("dbo.AspNetUsers");
			DropTable("dbo.Posts");
		}
	}
}