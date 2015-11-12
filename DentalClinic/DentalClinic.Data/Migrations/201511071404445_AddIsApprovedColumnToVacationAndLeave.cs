namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsApprovedColumnToVacationAndLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "IsApproved", c => c.Boolean());
            AddColumn("dbo.Vacations", "IsApproved", c => c.Boolean());
            DropColumn("dbo.Leaves", "IsDeleted");
            DropColumn("dbo.Vacations", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vacations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Leaves", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Vacations", "IsApproved");
            DropColumn("dbo.Leaves", "IsApproved");
        }
    }
}
