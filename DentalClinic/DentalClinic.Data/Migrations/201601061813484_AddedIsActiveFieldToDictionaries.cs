namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActiveFieldToDictionaries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VacationTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeaveTypes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeaveTypes", "IsActive");
            DropColumn("dbo.VacationTypes", "IsActive");
        }
    }
}
