using System;

namespace DentalClinic.Services.Helpers
{
    public class VacationToAdd
    {
        public int UserId { get; set; }
        public int VacationTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
    }
}
