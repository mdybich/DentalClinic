using System.Collections.Generic;

namespace DentalClinic.Data.Models
{
    public class VacationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
