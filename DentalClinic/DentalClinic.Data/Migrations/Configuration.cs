using DentalClinic.Data.Models;

namespace DentalClinic.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DentalClinic.Data.DentalClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DentalClinic.Data.DentalClinicContext context)
        {
            //SeedRoles(context);
            //SeedLeaveTypes(context);
            //SeedVacationTypes(context);
            //SeedUsers(context);
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
                    Name = "Chorobowe",
                    IsActive = true
                    
                },
                new LeaveType()
                {
                    Id = 2,
                    Name = "Opieka nad dzieckiem",
                    IsActive = true
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
                    Name = @"Wypoczynkowy",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 2,
                    Name = @"Okolicznościowy",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 3,
                    Name = @"Na żądanie",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 4,
                    Name = @"Bezpłatny",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 5,
                    Name = @"Macierzyński",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 6,
                    Name = @"Ojcowski",
                    IsActive = true
                },
                new VacationType()
                {
                    Id = 7,
                    Name = @"Wychowawczy",
                    IsActive = true
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
                    FirstName = "Michal",
                    LastName = "Dybich",
                    IsActive = true,
                    HashedPassword = @"/rFkWu74lVGMPMCGO6Lt+wuZ2zdW2qACxQimd55Wc2c=",
                    RoleId = 1,
                },
                new User()
                {
                    Id = 3,
                    Login = "kblazelek",
                    FirstName = "Krzysztof",
                    LastName = "Blazelek",
                    IsActive = true,
                    HashedPassword = @"OVOOHIGx2xyoNjoXUddBrMesR68G2rDuE+9O2NtQTG0=",
                    RoleId = 2,
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
