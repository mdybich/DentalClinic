using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Data.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }

        public int VacationTypeId { get; set; }
        public virtual VacationType Type { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int SubstituteUserId { get; set; }
        public virtual User SubstituteUser { get; set; }
    }
}
