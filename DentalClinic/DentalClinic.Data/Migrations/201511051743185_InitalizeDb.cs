namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalizeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        LeaveTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.LeaveTypeId);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        HashedPassword = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        VacationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VacationTypes", t => t.VacationTypeId, cascadeDelete: true)
                .Index(t => t.VacationTypeId);
            
            CreateTable(
                "dbo.VacationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacations", "VacationTypeId", "dbo.VacationTypes");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropIndex("dbo.Vacations", new[] { "VacationTypeId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            DropTable("dbo.VacationTypes");
            DropTable("dbo.Vacations");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.Leaves");
        }
    }
}
