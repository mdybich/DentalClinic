using DentalClinic.Data;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data.Models;

namespace DentalClinic.Services.Services
{
    public class LeaveService : ILeaveService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public IEnumerable<LeaveToDisplay> GetAllLeaves()
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

        public IEnumerable<LeaveTypeToDisplay> GetAllLeavesTypes()
        {
            var leavesTypes = db.LeaveTypes.Select(l => new LeaveTypeToDisplay
            {
                Id = l.Id,
                Name = l.Name
            });

            return leavesTypes;
        }

        public LeaveToDisplay AddLeave(LeaveToAdd leaveToAdd)
        {
            var substituteUsersId = db.Users.Where(u => u.Id != leaveToAdd.UserId).Select(u => u.Id).FirstOrDefault();

            var leave = new Leave
            {
                UserId = leaveToAdd.UserId,
                LeaveTypeId = leaveToAdd.LeaveTypeId,
                SubstituteUserId = substituteUsersId,
                StartDate = leaveToAdd.StartDate,
                EndDate = leaveToAdd.EndDate,
                Comment = leaveToAdd.Comment,
            };

            db.Leaves.Add(leave);
            db.SaveChanges();

            var newLeave = db.Leaves.Include("User").Include("Type").Include("SubstituteUser")
                .Where(l => l.Id == leave.Id).ToList().Select(l => new LeaveToDisplay
                {
                    UserFullName = $"{l.User.FirstName} {l.User.LastName}",
                    StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                    EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                    Comment = l.Comment,
                    LeaveTypeName = l.Type.Name,
                    SubstituteUserFullName = $"{l.SubstituteUser.FirstName} {l.SubstituteUser.LastName}"
                }).FirstOrDefault();

            return newLeave;
        } 
    }
}
