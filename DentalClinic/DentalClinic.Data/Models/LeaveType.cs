using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Data.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Leave> Leaves { get; set; }
    }
}
