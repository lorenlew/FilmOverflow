using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class ticketSeatChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservedSeats", "Id", "dbo.Tickets");
            DropIndex("dbo.ReservedSeats", new[] { "Id" });
            DropPrimaryKey("dbo.ReservedSeats");
            AddColumn("dbo.Tickets", "SeatId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReservedSeats", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ReservedSeats", "Id");
            DropColumn("dbo.Tickets", "ReservedSeatId");
            DropColumn("dbo.ReservedSeats", "IsSold");
            DropColumn("dbo.ReservedSeats", "TicketId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservedSeats", "TicketId", c => c.Long());
            AddColumn("dbo.ReservedSeats", "IsSold", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tickets", "ReservedSeatId", c => c.Long(nullable: false));
            DropPrimaryKey("dbo.ReservedSeats");
            AlterColumn("dbo.ReservedSeats", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.Tickets", "SeatId");
            AddPrimaryKey("dbo.ReservedSeats", "Id");
            CreateIndex("dbo.ReservedSeats", "Id");
            AddForeignKey("dbo.ReservedSeats", "Id", "dbo.Tickets", "Id");
        }
    }
}
