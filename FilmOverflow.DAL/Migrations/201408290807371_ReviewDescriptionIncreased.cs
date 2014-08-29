namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewDescriptionIncreased : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
