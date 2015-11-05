namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToVacationsRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacations", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Vacations", "SubstituteUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vacations", "UserId");
            CreateIndex("dbo.Vacations", "SubstituteUserId");
            AddForeignKey("dbo.Vacations", "SubstituteUserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Vacations", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Vacations", "SubstituteUserId", "dbo.Users");
            DropIndex("dbo.Vacations", new[] { "SubstituteUserId" });
            DropIndex("dbo.Vacations", new[] { "UserId" });
            DropColumn("dbo.Vacations", "SubstituteUserId");
            DropColumn("dbo.Vacations", "UserId");
        }
    }
}
