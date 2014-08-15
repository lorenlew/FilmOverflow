namespace FilmOverflow.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedmisstapes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "PaymentMethod_Id", c => c.Guid());
            CreateIndex("dbo.Tickets", "PaymentMethod_Id");
            AddForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropIndex("dbo.Tickets", new[] { "PaymentMethod_Id" });
            DropColumn("dbo.Tickets", "PaymentMethod_Id");
            DropTable("dbo.PaymentMethods");
        }
    }
}
