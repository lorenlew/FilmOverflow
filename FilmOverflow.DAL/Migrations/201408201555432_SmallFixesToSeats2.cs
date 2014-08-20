namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallFixesToSeats2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Hall_Id", "dbo.Halls");
            DropIndex("dbo.Seats", new[] { "Hall_Id" });
            DropColumn("dbo.Seats", "HallId");
            RenameColumn(table: "dbo.Seats", name: "Hall_Id", newName: "HallId");
            AlterColumn("dbo.Seats", "HallId", c => c.Long(nullable: false));
            AlterColumn("dbo.Seats", "HallId", c => c.Long(nullable: false));
            CreateIndex("dbo.Seats", "HallId");
            AddForeignKey("dbo.Seats", "HallId", "dbo.Halls", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "HallId", "dbo.Halls");
            DropIndex("dbo.Seats", new[] { "HallId" });
            AlterColumn("dbo.Seats", "HallId", c => c.Long());
            AlterColumn("dbo.Seats", "HallId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Seats", name: "HallId", newName: "Hall_Id");
            AddColumn("dbo.Seats", "HallId", c => c.Int(nullable: false));
            CreateIndex("dbo.Seats", "Hall_Id");
            AddForeignKey("dbo.Seats", "Hall_Id", "dbo.Halls", "Id");
        }
    }
}
