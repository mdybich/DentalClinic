using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;

namespace DentalClinic.Services.Services
{
    public class UserService : IUserService
    {
        private DentalClinicContext db = new DentalClinicContext();
        public IEnumerable<BasicUserToDisplay> GetBasicUsers()
        {
            var users = db.Users.ToList().Select(u => new BasicUserToDisplay
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}"
            });

            return users;
        }
    }
}
