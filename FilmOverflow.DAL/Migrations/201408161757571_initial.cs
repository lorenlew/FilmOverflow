using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Cinemas",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 30),
						ImagePath = c.String(nullable: false),
						Address = c.String(nullable: false, maxLength: 60),
						PhoneNumber = c.String(nullable: false, maxLength: 60),
					})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Seances",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						FilmId = c.Long(nullable: false),
						CinemaId = c.Long(nullable: false),
						SeanceDate = c.DateTime(nullable: false),
						Price = c.Decimal(nullable: false, precision: 18, scale: 2),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
				.ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
				.Index(t => t.FilmId)
				.Index(t => t.CinemaId);

			CreateTable(
				"dbo.Tickets",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						ApplicationUserId = c.String(maxLength: 128),
						SeanceId = c.Long(nullable: false),
						SeatNumber = c.Int(nullable: false),
						PaymentMethodId = c.Long(nullable: false),
						PaymentDate = c.DateTime(nullable: false),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Seances", t => t.SeanceId, cascadeDelete: true)
				.ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId, cascadeDelete: true)
				.ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
				.Index(t => t.ApplicationUserId)
				.Index(t => t.SeanceId)
				.Index(t => t.PaymentMethodId);

			CreateTable(
				"dbo.Films",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						Title = c.String(nullable: false, maxLength: 60),
						Description = c.String(nullable: false, maxLength: 1000),
						ImagePath = c.String(nullable: false),
						Duration = c.String(nullable: false, maxLength: 60),
					})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Reviews",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						ApplicationUserId = c.String(maxLength: 128),
						FilmId = c.Long(nullable: false),
						Rate = c.Int(nullable: false),
						Description = c.String(nullable: false, maxLength: 1000),
						ReviewDate = c.DateTime(nullable: false),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
				.ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
				.Index(t => t.ApplicationUserId)
				.Index(t => t.FilmId);

			CreateTable(
				"dbo.PaymentMethods",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						Name = c.String(nullable: false, maxLength: 30),
					})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.AspNetRoles",
				c => new
					{
						Id = c.String(nullable: false, maxLength: 128),
						Name = c.String(nullable: false, maxLength: 256),
					})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Name, unique: true, name: "RoleNameIndex");

			CreateTable(
				"dbo.AspNetUserRoles",
				c => new
					{
						UserId = c.String(nullable: false, maxLength: 128),
						RoleId = c.String(nullable: false, maxLength: 128),
					})
				.PrimaryKey(t => new { t.UserId, t.RoleId })
				.ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.RoleId);

			CreateTable(
				"dbo.AspNetUsers",
				c => new
					{
						Id = c.String(nullable: false, maxLength: 128),
						FirstName = c.String(nullable: false, maxLength: 30),
						LastName = c.String(nullable: false, maxLength: 30),
						IsBanned = c.Boolean(nullable: false),
						Email = c.String(maxLength: 256),
						EmailConfirmed = c.Boolean(nullable: false),
						PasswordHash = c.String(),
						SecurityStamp = c.String(),
						PhoneNumber = c.String(),
						PhoneNumberConfirmed = c.Boolean(nullable: false),
						TwoFactorEnabled = c.Boolean(nullable: false),
						LockoutEndDateUtc = c.DateTime(),
						LockoutEnabled = c.Boolean(nullable: false),
						AccessFailedCount = c.Int(nullable: false),
						UserName = c.String(nullable: false, maxLength: 256),
					})
				.PrimaryKey(t => t.Id)
				.Index(t => t.UserName, unique: true, name: "UserNameIndex");

			CreateTable(
				"dbo.AspNetUserClaims",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						UserId = c.String(nullable: false, maxLength: 128),
						ClaimType = c.String(),
						ClaimValue = c.String(),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.AspNetUserLogins",
				c => new
					{
						LoginProvider = c.String(nullable: false, maxLength: 128),
						ProviderKey = c.String(nullable: false, maxLength: 128),
						UserId = c.String(nullable: false, maxLength: 128),
					})
				.PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
				.ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

		}

		public override void Down()
		{
			DropForeignKey("dbo.Tickets", "ApplicationUserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.Reviews", "ApplicationUserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
			DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
			DropForeignKey("dbo.Tickets", "PaymentMethodId", "dbo.PaymentMethods");
			DropForeignKey("dbo.Seances", "FilmId", "dbo.Films");
			DropForeignKey("dbo.Reviews", "FilmId", "dbo.Films");
			DropForeignKey("dbo.Seances", "CinemaId", "dbo.Cinemas");
			DropForeignKey("dbo.Tickets", "SeanceId", "dbo.Seances");
			DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
			DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
			DropIndex("dbo.AspNetUsers", "UserNameIndex");
			DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
			DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
			DropIndex("dbo.AspNetRoles", "RoleNameIndex");
			DropIndex("dbo.Reviews", new[] { "FilmId" });
			DropIndex("dbo.Reviews", new[] { "ApplicationUserId" });
			DropIndex("dbo.Tickets", new[] { "PaymentMethodId" });
			DropIndex("dbo.Tickets", new[] { "SeanceId" });
			DropIndex("dbo.Tickets", new[] { "ApplicationUserId" });
			DropIndex("dbo.Seances", new[] { "CinemaId" });
			DropIndex("dbo.Seances", new[] { "FilmId" });
			DropTable("dbo.AspNetUserLogins");
			DropTable("dbo.AspNetUserClaims");
			DropTable("dbo.AspNetUsers");
			DropTable("dbo.AspNetUserRoles");
			DropTable("dbo.AspNetRoles");
			DropTable("dbo.PaymentMethods");
			DropTable("dbo.Reviews");
			DropTable("dbo.Films");
			DropTable("dbo.Tickets");
			DropTable("dbo.Seances");
			DropTable("dbo.Cinemas");
		}
	}
}
