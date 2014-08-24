using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class ticketreservedSeat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "ReservedSeatId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "ReservedSeatId");
        }
    }
}
