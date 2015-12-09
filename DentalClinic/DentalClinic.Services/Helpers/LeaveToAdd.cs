using System;

namespace DentalClinic.Services.Helpers
{
    public class LeaveToAdd
    {
        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
    }
}
