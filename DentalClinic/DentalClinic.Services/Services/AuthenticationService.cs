using System;
using System.Linq;
using System.Text;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using System.Security.Cryptography;
using DentalClinic.Data;

namespace DentalClinic.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public AuthenticationService()
        {
        }

        public UserInfo AuthenticateUser(string userName, string clearTextPassword)
        {

            var hashedPassword = HashHelper.CalculateHash(clearTextPassword, userName);


            var userData = db.Users.FirstOrDefault(u => u.Login.Equals(userName)
                                                     &&
                                                     u.HashedPassword == hashedPassword
                                                     && u.IsActive == true);

            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new UserInfo(userData.Login, userData.FirstName, userData.LastName, userData.Role);
        }
    }
}
