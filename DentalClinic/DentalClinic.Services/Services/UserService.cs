using System;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Data.Models;
using DentalClinic.Services.Exceptions;

namespace DentalClinic.Services.Services
{
    public class UserService : IUserService
    {
        private DentalClinicContext db = new DentalClinicContext();


        public IEnumerable<BasicUserToDisplay> GetBasicUsers()
        {
            var users = db.Users
                            .Where(u => u.IsActive == true)
                            .ToList()
                            .Select(u => new BasicUserToDisplay
                            {
                                Id = u.Id,
                                FullName = $"{u.FirstName} {u.LastName}"
                            });

            return users;
        }

        public IEnumerable<UserToDisplay> GetUsers()
        {
            var users = db.Users
                            .Where(u => u.IsActive == true)
                            .ToList()
                            .Select(u => new UserToDisplay
                            {
                                Login = u.Login,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                RoleName = u.Role.Name
                            });

            return users;
        }

        public void DeleteUser(string userLogin)
        {
            var userToDelete = db.Users.FirstOrDefault(u => u.Login == userLogin && u.IsActive);

            if (userToDelete == null)
            {
                throw new Exception("Nie znaleziono użytkownika do usunięcia");
            }

            userToDelete.IsActive = false;

            db.SaveChanges();

        }

        public IEnumerable<RoleToDisplay> GetRoles()
        {
            var roles = db.Roles
                            .Select(r => new RoleToDisplay
                            {
                                Id = r.Id,
                                Name = r.Name
                            });

            return roles;
        }

        public void AddUser(UserToAdd user)
        {
            if (user.Password != user.PasswordConfirmation)
            {
                throw new UserException("Podane hasła się nie zgadzają.");
            }

            if (db.Users.Any(u => u.Login == user.Login && u.IsActive == true))
            {
                throw new UserException("W systemie istnieje już użytkownik o podanym loginie.");
            }

            if (!db.Roles.Any(r => r.Id == user.RoleId))
            {
                throw new UserException("Nieprawidłowa rola.");
            }

            var hashedPassword = HashHelper.CalculateHash(user.Password, user.Login);

            var userToAdd = new User
            {
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HashedPassword = hashedPassword,
                RoleId = user.RoleId,
                IsActive = true
            };

            db.Users.Add(userToAdd);

            db.SaveChanges();

        }
    }
}
