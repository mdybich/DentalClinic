using DentalClinic.Data.Models;

namespace DentalClinic.Services.Helpers
{
    public class UserInfo
    {
        public UserInfo(string login, string firstName, string lastName, Role role)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}
