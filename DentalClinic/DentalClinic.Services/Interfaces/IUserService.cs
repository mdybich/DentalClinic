using System.Collections.Generic;
using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<BasicUserToDisplay> GetBasicUsers();
        IEnumerable<UserToDisplay> GetUsers();
        IEnumerable<RoleToDisplay> GetRoles();
        void DeleteUser(string userLogin);
        void AddUser(UserToAdd user);
    }
}
