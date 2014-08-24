using System.Data.Entity.Migrations;

namespace FilmOverflow.DAL.Migrations
{
	public partial class FilmTableDurationToIntAndSeanceTableFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "Duration", c => c.String(nullable: false, maxLength: 60));
        }
    }
}
