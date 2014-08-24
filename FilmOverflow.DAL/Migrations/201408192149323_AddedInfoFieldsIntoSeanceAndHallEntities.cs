using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class AddedInfoFieldsIntoSeanceAndHallEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Halls", "HallNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Seances", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Seances", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Seances", "SeanceDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seances", "SeanceDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Seances", "Time");
            DropColumn("dbo.Seances", "Date");
            DropColumn("dbo.Halls", "HallNumber");
        }
    }
}
