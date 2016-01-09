namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalizeData : DbMigration
    {
        public override void Up()
        {
            Sql
                (
                @"USE [DentalClinic]
                GO
                SET IDENTITY_INSERT [dbo].[VacationTypes] ON 

                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (1, N'Wypoczynkowy', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (2, N'Okolicznosciowy', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (3, N'Na zadanie', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (4, N'Bezplatny', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (5, N'Macierzynski', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (6, N'Ojcowski', 1)
                INSERT [dbo].[VacationTypes] ([Id], [Name], [IsActive]) VALUES (7, N'Wychowawczy', 1)
                SET IDENTITY_INSERT [dbo].[VacationTypes] OFF"
                );

            Sql
                (
                @"USE [DentalClinic]
                GO
                SET IDENTITY_INSERT [dbo].[LeaveTypes] ON 

                INSERT [dbo].[LeaveTypes] ([Id], [Name], [IsActive]) VALUES (1, N'Chorobowe', 1)
                INSERT [dbo].[LeaveTypes] ([Id], [Name], [IsActive]) VALUES (2, N'Opieka nad dzieckiem', 1)
                SET IDENTITY_INSERT [dbo].[LeaveTypes] OFF"
                );

            Sql
                (
                @"USE [DentalClinic]
                GO
                SET IDENTITY_INSERT [dbo].[Roles] ON 

                INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Administrator')
                INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Pracownik')
                INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Rejestratorka')
                INSERT [dbo].[Roles] ([Id], [Name]) VALUES (4, N'Kierownik')
                SET IDENTITY_INSERT [dbo].[Roles] OFF"
                );

            Sql
                (
                @"USE [DentalClinic]
                GO
                SET IDENTITY_INSERT [dbo].[Users] ON 

                INSERT [dbo].[Users] ([Id], [Login], [FirstName], [LastName], [HashedPassword], [IsActive], [RoleId]) VALUES (1, N'admin', N'Administrator', N'Startowy', N'2CSU8F1pF7oC96qilonMtES7c/IDgIdssF0fN1N7eJI=', 1, 1)
                SET IDENTITY_INSERT [dbo].[Users] OFF"
                );
        }
        
        public override void Down()
        {
        }
    }
}
