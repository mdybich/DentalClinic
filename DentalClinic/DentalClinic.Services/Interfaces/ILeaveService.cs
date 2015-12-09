using DentalClinic.Services.Helpers;
using System.Collections.Generic;

namespace DentalClinic.Services.Interfaces
{
    public interface ILeaveService
    {
        IEnumerable<LeaveToDisplay> GetAllLeaves();
        IEnumerable<LeaveTypeToDisplay> GetAllLeavesTypes();
        LeaveToDisplay AddLeave(LeaveToAdd leave);
    }
}
