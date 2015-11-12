using System.Collections.Generic;
using DentalClinic.Data.Models;

namespace DentalClinic.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DentalClinic.Data.DentalClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DentalClinic.Data.DentalClinicContext context)
        {
            SeedRoles(context);
            SeedLeaveTypes(context);
            SeedVacationTypes(context);
            SeedUsers(context);
        }

        private void SeedRoles(DentalClinicContext context)
        {
            var rolesArray = new Role[]
            {
                new Role()
                {
                    Id = 1,
                    Name = "Administrator"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Pracownik",
                },
                new Role()
                {
                    Id = 3,
                    Name = "Rejestratorka"
                },
                new Role()
                {
                    Id = 4,
                    Name = "Kierownik"
                }
            };
            context.Set<Role>().AddOrUpdate(rolesArray);
            context.SaveChanges();
        }

        private void SeedLeaveTypes(DentalClinicContext context)
        {
            var leaveTypesArray = new LeaveType[]
            {
                new LeaveType()
                {
                    Id = 1,
                    Name = "Chorobowe"
                },
                new LeaveType()
                {
                    Id = 2,
                    Name = "Opieka nad dzieckiem"
                }
            };
            context.Set<LeaveType>().AddOrUpdate(leaveTypesArray);
            context.SaveChanges();
        }

        private void SeedVacationTypes(DentalClinicContext context)
        {
            var vacationTypesArray = new VacationType[]
            {
                new VacationType()
                {
                    Id = 1,
                    Name = "Wypoczynkowy"
                },
                new VacationType()
                {
                    Id = 2,
                    Name = "Okoliczno�ciowy"
                },
                new VacationType()
                {
                    Id = 3,
                    Name = "Na ��danie"
                },
                new VacationType()
                {
                    Id = 4,
                    Name = "Bezp�atny"
                },
                new VacationType()
                {
                    Id = 5,
                    Name = "Macierzy�ski"
                },
                new VacationType()
                {
                    Id = 6,
                    Name = "Ojcowski"
                },
                new VacationType()
                {
                    Id = 7,
                    Name = "Wychowawczy"
                }
            };

            context.Set<VacationType>().AddOrUpdate(vacationTypesArray);
            context.SaveChanges();
        }

        private void SeedUsers(DentalClinicContext context)
        {
            var usersArray = new User[]
            {
                new User()
                {
                    Id = 1,
                    Login = "mdybich",
                    FirstName = "Micha�",
                    LastName = "Dybich",
                    IsActive = true,
                    HashedPassword = @"/rFkWu74lVGMPMCGO6Lt+wuZ2zdW2qACxQimd55Wc2c=",
                    RoleId = 1,
                },
                new User()
                {
                    Id = 2,
                    Login = "mgrzybek",
                    FirstName = "Marcin",
                    LastName = "Grzybek",
                    IsActive = true,
                    HashedPassword = "qGdUzgNz7lITjrVF3bqVPTb2sX+Vl7hUNAIMv0wbM5M=",
                    RoleId = 3
                }
            };

            context.Set<User>().AddOrUpdate(usersArray);
            context.SaveChanges();
        }
    }
}