using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public bool IsActive { get; set; }

        public int RoleId  { get; set; }
        public Role Role { get; set; }
    }
}
