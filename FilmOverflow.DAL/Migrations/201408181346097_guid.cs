namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas");
            DropForeignKey("dbo.Tickets", "Seance_Id", "dbo.Seances");
            DropForeignKey("dbo.Reviews", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Seances", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropPrimaryKey("dbo.Cinemas");
            DropPrimaryKey("dbo.Seances");
            DropPrimaryKey("dbo.Tickets");
            DropPrimaryKey("dbo.Films");
            DropPrimaryKey("dbo.Reviews");
            DropPrimaryKey("dbo.PaymentMethods");
            AlterColumn("dbo.Cinemas", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Seances", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Tickets", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Films", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Reviews", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.PaymentMethods", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cinemas", "Id");
            AddPrimaryKey("dbo.Seances", "Id");
            AddPrimaryKey("dbo.Tickets", "Id");
            AddPrimaryKey("dbo.Films", "Id");
            AddPrimaryKey("dbo.Reviews", "Id");
            AddPrimaryKey("dbo.PaymentMethods", "Id");
            AddForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas", "Id");
            AddForeignKey("dbo.Tickets", "Seance_Id", "dbo.Seances", "Id");
            AddForeignKey("dbo.Reviews", "Film_Id", "dbo.Films", "Id");
            AddForeignKey("dbo.Seances", "Film_Id", "dbo.Films", "Id");
            AddForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropForeignKey("dbo.Seances", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Reviews", "Film_Id", "dbo.Films");
            DropForeignKey("dbo.Tickets", "Seance_Id", "dbo.Seances");
            DropForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas");
            DropPrimaryKey("dbo.PaymentMethods");
            DropPrimaryKey("dbo.Reviews");
            DropPrimaryKey("dbo.Films");
            DropPrimaryKey("dbo.Tickets");
            DropPrimaryKey("dbo.Seances");
            DropPrimaryKey("dbo.Cinemas");
            AlterColumn("dbo.PaymentMethods", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Reviews", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Films", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Tickets", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Seances", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Cinemas", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PaymentMethods", "Id");
            AddPrimaryKey("dbo.Reviews", "Id");
            AddPrimaryKey("dbo.Films", "Id");
            AddPrimaryKey("dbo.Tickets", "Id");
            AddPrimaryKey("dbo.Seances", "Id");
            AddPrimaryKey("dbo.Cinemas", "Id");
            AddForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods", "Id");
            AddForeignKey("dbo.Seances", "Film_Id", "dbo.Films", "Id");
            AddForeignKey("dbo.Reviews", "Film_Id", "dbo.Films", "Id");
            AddForeignKey("dbo.Tickets", "Seance_Id", "dbo.Seances", "Id");
            AddForeignKey("dbo.Seances", "Cinema_Id", "dbo.Cinemas", "Id");
        }
    }
}
