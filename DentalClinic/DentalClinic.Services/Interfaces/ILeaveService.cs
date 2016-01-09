using DentalClinic.Services.Helpers;
using System.Collections.Generic;

namespace DentalClinic.Services.Interfaces
{
    public interface ILeaveService
    {
        IEnumerable<LeaveToDisplay> GetAllLeaves();
        IEnumerable<LeaveToDisplay> GetLeavesByLogin(string login);
        IEnumerable<LeaveTypeToDisplay> GetAllLeavesTypes();
        void AddLeave(LeaveToAdd leave);
        void DeleteLeaveType(int id);
        void AddLeaveType(string leaveTypeName);
    }
}
