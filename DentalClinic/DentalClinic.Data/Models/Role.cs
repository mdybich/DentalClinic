using System.Collections.Generic;
using System.Runtime;

namespace DentalClinic.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}