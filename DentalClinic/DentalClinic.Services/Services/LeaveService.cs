using DentalClinic.Data;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data.Models;
using System;
using DentalClinic.Services.Exceptions;

namespace DentalClinic.Services.Services
{
    public class LeaveService : ILeaveService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public IEnumerable<LeaveToDisplay> GetAllLeaves()
        {
            var leaves = db.Leaves
                            .ToList()
                            .Select(l => new LeaveToDisplay
                            {
                                UserFullName = l.User.FirstName + " " + l.User.LastName,
                                StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                                EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                                Comment = l.Comment,
                                LeaveTypeName = l.Type.Name,
                                SubstituteUserFullName = l.SubstituteUser.FirstName + " " + l.SubstituteUser.LastName,
                                IsApproved = l.IsApproved != null ? (l.IsApproved == true ? "Tak" : "Nie") : "Oczekuje"
                            });

            return leaves;
        }

        public IEnumerable<LeaveToDisplay> GetLeavesByLogin(string login)
        {
            var leaves = db.Leaves
                            .Where(l => l.User.Login == login)
                            .ToList()
                            .Select(l => new LeaveToDisplay
                            {
                                UserFullName = l.User.FirstName + " " + l.User.LastName,
                                StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                                EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                                Comment = l.Comment,
                                LeaveTypeName = l.Type.Name,
                                SubstituteUserFullName = l.SubstituteUser.FirstName + " " + l.SubstituteUser.LastName,
                                IsApproved = l.IsApproved != null ? (l.IsApproved == true ? "Tak" : "Nie") : "Oczekuje"
                            });

            return leaves;
        }

        public IEnumerable<LeaveTypeToDisplay> GetAllLeavesTypes()
        {
            var leavesTypes = db.LeaveTypes
                                    .Where(l => l.IsActive == true)
                                    .Select(l => new LeaveTypeToDisplay
                                    {
                                        Id = l.Id,
                                        Name = l.Name
                                    });

            return leavesTypes;
        }

        public void AddLeave(LeaveToAdd leaveToAdd)
        {
            var isUserAlreadyHaveLeave = db.Leaves
                                            .Any(l => l.StartDate < leaveToAdd.EndDate
                                            && l.EndDate > leaveToAdd.StartDate
                                            && l.UserId == leaveToAdd.UserId);

            if (isUserAlreadyHaveLeave)
            {
                var userDisplayName = db.Users
                                        .Where(u => u.Id == leaveToAdd.UserId)
                                        .ToList()
                                        .Select(u => string.Format("{0} {1}", u.FirstName, u.LastName))
                                        .SingleOrDefault();

                var message = string.Format("{0} w podanym terminie przebywa już na zwolnieniu.", userDisplayName);

                throw new LeaveException(message);
            }

            var isUserAlreadyHaveSubstitution = db.Leaves
                                                    .Any(l => l.StartDate < leaveToAdd.EndDate
                                                    && l.EndDate > leaveToAdd.StartDate
                                                    && l.SubstituteUserId == leaveToAdd.UserId);

            if (isUserAlreadyHaveSubstitution)
            {
                var userDisplayName = db.Users
                                        .Where(u => u.Id == leaveToAdd.UserId)
                                        .ToList()
                                        .Select(u => string.Format("{0} {1}", u.FirstName, u.LastName))
                                        .SingleOrDefault();

                var message = string.Format("{0} w podanym terminie przebywa na zastępstwie.", userDisplayName);

                throw new LeaveException(message);
            }


            var usersIdOnSubstitution = db.Leaves
                                            .Where(l => l.StartDate < leaveToAdd.EndDate
                                            && l.EndDate > leaveToAdd.StartDate
                                            && l.SubstituteUserId != leaveToAdd.UserId)
                                            .Select(l => l.SubstituteUserId);


            var userRoleId = db.Users
                                .Where(u => u.Id == leaveToAdd.UserId)
                                .Select(u => u.RoleId)
                                .SingleOrDefault();

            var substituteUsersId = db.Users
                                        .Where(u => u.Id != leaveToAdd.UserId
                                        &&
                                        u.RoleId == userRoleId
                                        &&
                                        u.IsActive == true
                                        &&
                                        !u.Leaves.Any(l => l.StartDate < leaveToAdd.EndDate
                                                        && l.EndDate > leaveToAdd.StartDate))
                                        .Select(u => u.Id);

            var substituteUserId = substituteUsersId
                                    .Except(usersIdOnSubstitution)
                                    .FirstOrDefault();

            if (substituteUserId == default(int))
            {
                throw new LeaveException("W podanym terminie nie można znaleźć zastępstwa.");
            }

            var leave = new Leave
            {
                UserId = leaveToAdd.UserId,
                LeaveTypeId = leaveToAdd.LeaveTypeId,
                SubstituteUserId = substituteUserId,
                StartDate = leaveToAdd.StartDate,
                EndDate = leaveToAdd.EndDate,
                Comment = leaveToAdd.Comment,
            };

            db.Leaves.Add(leave);
            db.SaveChanges();
        }

        public void DeleteLeaveType(int id)
        {
            var leaveType = db.LeaveTypes.FirstOrDefault(l => l.Id == id && l.IsActive);

            if (leaveType == null)
            {
                throw new Exception("Typ zwolnienia nie istnieje");
            }

            leaveType.IsActive = false;

            db.SaveChanges();
        }

        public void AddLeaveType(string leaveTypeName)
        {
            if (string.IsNullOrEmpty(leaveTypeName) || string.IsNullOrWhiteSpace(leaveTypeName))
            {
                throw new LeaveTypeException("Nie można dodać rodzaju zwolnienia o pustej nazwie.");
            }

            if (db.LeaveTypes.FirstOrDefault(v => v.Name.ToLower() == leaveTypeName.ToLower()) != null)
            {
                throw new LeaveTypeException("Podany rodzaj zwolnienia już istnieje.");
            }

            var leaveTypeToAdd = new LeaveType();
            leaveTypeToAdd.Name = leaveTypeName;
            leaveTypeToAdd.IsActive = true;

            db.LeaveTypes.Add(leaveTypeToAdd);

            db.SaveChanges();
        }
    }
}
