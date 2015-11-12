using DentalClinic.Data.Models;
using System.Security.Principal;

namespace DentalClinic.UI.Authorization
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string firstName, string lastName, Role role)
        {
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public string AuthenticationType => "Custom Authentication";

        public bool IsAuthenticated => !string.IsNullOrEmpty(Name);
    }
}
