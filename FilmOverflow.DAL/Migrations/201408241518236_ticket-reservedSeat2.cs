using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class ticketreservedSeat2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservedSeats", "TicketId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservedSeats", "TicketId");
        }
    }
}
