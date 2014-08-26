namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSoldAndApplUserInReservedSeat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservedSeats", "IsSold", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservedSeats", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ReservedSeats", "ApplicationUserId");
            AddForeignKey("dbo.ReservedSeats", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservedSeats", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ReservedSeats", new[] { "ApplicationUserId" });
            DropColumn("dbo.ReservedSeats", "ApplicationUserId");
            DropColumn("dbo.ReservedSeats", "IsSold");
        }
    }
}
