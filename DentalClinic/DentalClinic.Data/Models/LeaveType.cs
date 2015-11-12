using System.Collections.Generic;

namespace DentalClinic.Data.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Leave> Leaves { get; set; }
    }
}
