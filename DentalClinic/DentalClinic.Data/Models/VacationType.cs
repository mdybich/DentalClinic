using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Data.Models
{
    public class VacationType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
