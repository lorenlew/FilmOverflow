namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallFixesToSeats : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HallCinemas", "Hall_Id", "dbo.Halls");
            DropForeignKey("dbo.HallCinemas", "Cinema_Id", "dbo.Cinemas");
            DropForeignKey("dbo.Seances", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.Seances", new[] { "CinemaId" });
            DropIndex("dbo.HallCinemas", new[] { "Hall_Id" });
            DropIndex("dbo.HallCinemas", new[] { "Cinema_Id" });
            RenameColumn(table: "dbo.Seances", name: "CinemaId", newName: "Cinema_Id");
            AddColumn("dbo.Halls", "Name", c => c.String(nullable: false, maxLength: 60));
            AddColumn("dbo.Halls", "CinemaId", c => c.Long(nullable: false));
            AddColumn("dbo.ReservedSeats", "IsSold", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservedSeats", "ReservationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Seances", "Cinema_Id", c => c.Long());
            CreateIndex("dbo.Halls", "CinemaId");
            CreateIndex("dbo.Seances", "Cinema_Id");
            AddForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas", "Id");
            DropColumn("dbo.Halls", "HallNumber");
            DropColumn("dbo.Seats", "IsProcessing");
            DropTable("dbo.HallCinemas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HallCinemas",
                c => new
                    {
                        Hall_Id = c.Long(nullable: false),
                        Cinema_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hall_Id, t.Cinema_Id });
            
            AddColumn("dbo.Seats", "IsProcessing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Halls", "HallNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas");
            DropForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.Seances", new[] { "Cinema_Id" });
            DropIndex("dbo.Halls", new[] { "CinemaId" });
            AlterColumn("dbo.Seances", "Cinema_Id", c => c.Long(nullable: false));
            DropColumn("dbo.ReservedSeats", "ReservationTime");
            DropColumn("dbo.ReservedSeats", "IsSold");
            DropColumn("dbo.Halls", "CinemaId");
            DropColumn("dbo.Halls", "Name");
            RenameColumn(table: "dbo.Seances", name: "Cinema_Id", newName: "CinemaId");
            CreateIndex("dbo.HallCinemas", "Cinema_Id");
            CreateIndex("dbo.HallCinemas", "Hall_Id");
            CreateIndex("dbo.Seances", "CinemaId");
            AddForeignKey("dbo.Seances", "CinemaId", "dbo.Cinemas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HallCinemas", "Cinema_Id", "dbo.Cinemas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HallCinemas", "Hall_Id", "dbo.Halls", "Id", cascadeDelete: true);
        }
    }
}
