using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class AddedHallsAndSeats : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Halls",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						RowAmount = c.Int(nullable: false),
						ColumnAmount = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Seats",
				c => new
					{
						Id = c.Long(nullable: false, identity: true),
						RowNumber = c.Int(nullable: false),
						ColumnNumber = c.Int(nullable: false),
						HallId = c.Int(nullable: false),
						Hall_Id = c.Long(),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Halls", t => t.Hall_Id)
				.Index(t => t.Hall_Id);

			CreateTable(
				"dbo.ReservedSeats",
				c => new
					{
						Id = c.Long(nullable: false),
						SeanceId = c.Long(nullable: false),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Seances", t => t.SeanceId, cascadeDelete: true)
				.ForeignKey("dbo.Tickets", t => t.Id)
				.Index(t => t.Id)
				.Index(t => t.SeanceId);

			CreateTable(
				"dbo.HallCinemas",
				c => new
					{
						Hall_Id = c.Long(nullable: false),
						Cinema_Id = c.Long(nullable: false),
					})
				.PrimaryKey(t => new { t.Hall_Id, t.Cinema_Id })
				.ForeignKey("dbo.Halls", t => t.Hall_Id, cascadeDelete: true)
				.ForeignKey("dbo.Cinemas", t => t.Cinema_Id, cascadeDelete: true)
				.Index(t => t.Hall_Id)
				.Index(t => t.Cinema_Id);

			AddColumn("dbo.Seances", "HallId", c => c.Long(nullable: false));
			CreateIndex("dbo.Seances", "HallId");
			AddForeignKey("dbo.Seances", "HallId", "dbo.Halls", "Id", cascadeDelete: true);
			DropColumn("dbo.Tickets", "SeatNumber");
		}

		public override void Down()
		{
			AddColumn("dbo.Tickets", "SeatNumber", c => c.Int(nullable: false));
			DropForeignKey("dbo.Seances", "HallId", "dbo.Halls");
			DropForeignKey("dbo.ReservedSeats", "Id", "dbo.Tickets");
			DropForeignKey("dbo.ReservedSeats", "SeanceId", "dbo.Seances");
			DropForeignKey("dbo.Seats", "Hall_Id", "dbo.Halls");
			DropForeignKey("dbo.HallCinemas", "Cinema_Id", "dbo.Cinemas");
			DropForeignKey("dbo.HallCinemas", "Hall_Id", "dbo.Halls");
			DropIndex("dbo.HallCinemas", new[] { "Cinema_Id" });
			DropIndex("dbo.HallCinemas", new[] { "Hall_Id" });
			DropIndex("dbo.ReservedSeats", new[] { "SeanceId" });
			DropIndex("dbo.ReservedSeats", new[] { "Id" });
			DropIndex("dbo.Seances", new[] { "HallId" });
			DropIndex("dbo.Seats", new[] { "Hall_Id" });
			DropColumn("dbo.Seances", "HallId");
			DropTable("dbo.HallCinemas");
			DropTable("dbo.ReservedSeats");
			DropTable("dbo.Seats");
			DropTable("dbo.Halls");
		}
	}
}
