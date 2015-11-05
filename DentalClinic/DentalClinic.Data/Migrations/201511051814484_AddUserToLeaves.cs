namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToLeaves : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Leaves", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "SubstituteUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Vacations", "IsDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Leaves", "UserId");
            CreateIndex("dbo.Leaves", "SubstituteUserId");
            AddForeignKey("dbo.Leaves", "SubstituteUserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Leaves", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "UserId", "dbo.Users");
            DropForeignKey("dbo.Leaves", "SubstituteUserId", "dbo.Users");
            DropIndex("dbo.Leaves", new[] { "SubstituteUserId" });
            DropIndex("dbo.Leaves", new[] { "UserId" });
            DropColumn("dbo.Vacations", "IsDeleted");
            DropColumn("dbo.Leaves", "SubstituteUserId");
            DropColumn("dbo.Leaves", "UserId");
            DropColumn("dbo.Leaves", "IsDeleted");
        }
    }
}
