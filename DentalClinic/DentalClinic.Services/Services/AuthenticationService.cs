using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Data.Models;
using System.Security.Cryptography;

namespace DentalClinic.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private List<User> users;

        public AuthenticationService()
        {
            users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Login = "mdybich",
                    FirstName = "Michał",
                    LastName = "Dybich",
                    IsActive = true,
                    HashedPassword = @"/rFkWu74lVGMPMCGO6Lt+wuZ2zdW2qACxQimd55Wc2c=",
                    RoleId = 1,
                    Role = new Role()
                    {
                        Id = 1,
                        Name = "Administrator"
                    }
                }
            };
        }
        public UserInfo AuthenticateUser(string userName, string clearTextPassword)
        {
            var test = CalculateHash("qazwsx", "mdybich");
            var userData = users.FirstOrDefault(u => u.Login.Equals(userName)
                                                     &&
                                                     u.HashedPassword.Equals(CalculateHash(clearTextPassword, userName)));
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new UserInfo(userData.Login, userData.FirstName, userData.LastName, userData.Role);
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
