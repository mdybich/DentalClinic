using DentalClinic.Data;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DentalClinic.Services.Services
{
    public class LeaveService : ILeaveService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public IEnumerable<LeaveToDisplay> GetAll()
        {
            var leaves = db.Leaves.ToList().Select(l => new LeaveToDisplay
            {
                UserFullName = l.User.FirstName + " " + l.User.LastName,
                StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                Comment = l.Comment,
                LeaveTypeName = l.Type.Name,
                SubstituteUserFullName = l.SubstituteUser.FirstName + " " + l.SubstituteUser.LastName
            });

            return leaves;
        }
    }
}
