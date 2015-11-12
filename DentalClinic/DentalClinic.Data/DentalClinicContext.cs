using System.Data.Entity;
using DentalClinic.Data.Models;

namespace DentalClinic.Data
{
    public class DentalClinicContext : DbContext
    {
        public DentalClinicContext() : base ("DentalClinicContext")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacation>()
                .HasRequired(v => v.User)
                .WithMany(u => u.Vacations)
                .HasForeignKey(v => v.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vacation>()
                .HasRequired(v => v.SubstituteUser)
                .WithMany(u => u.SubstituteVacations)
                .HasForeignKey(v => v.SubstituteUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Leave>()
                .HasRequired(v => v.User)
                .WithMany(u => u.Leaves)
                .HasForeignKey(v => v.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Leave>()
                .HasRequired(v => v.SubstituteUser)
                .WithMany(u => u.SubstituteLeaves)
                .HasForeignKey(v => v.SubstituteUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
