using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Data.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }

        public int LeaveTypeId { get; set; }
        public virtual LeaveType Type { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int SubstituteUserId { get; set; }
        public virtual User SubstituteUser { get; set; }
    }
}
