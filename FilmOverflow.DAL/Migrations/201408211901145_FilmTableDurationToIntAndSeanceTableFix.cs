namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
