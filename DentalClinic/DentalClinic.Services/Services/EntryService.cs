using DentalClinic.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Services.Helpers;
using DentalClinic.Data;
using System;
using DentalClinic.Services.Exceptions;

namespace DentalClinic.Services.Services
{
    public class EntryService : IEntryService
    {
        private DentalClinicContext db = new DentalClinicContext();
        public IEnumerable<EntryToDisplay> GetAllPendingEntries()
        {
            var leaves = db.Leaves
                                .Where(l => l.IsApproved == null)
                                .ToList()
                                .Select(l => new EntryToDisplay
                                {
                                    Id = l.Id,
                                    StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                                    Comment = l.Comment,
                                    EntryType = EntryType.Leave,
                                    EntryTypeName = "Zwolnienie",
                                    UserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", l.SubstituteUser.FirstName, l.SubstituteUser.LastName)
                                });

            var vacations = db.Vacations
                                .Where(v => v.IsApproved == null)
                                .ToList()
                                .Select(v => new EntryToDisplay
                                {
                                    Id = v.Id,
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    Comment = v.Comment,
                                    EntryType = EntryType.Vacation,
                                    EntryTypeName = "Urlop",
                                    UserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", v.SubstituteUser.FirstName, v.SubstituteUser.LastName)
                                });

            var entries = leaves.Concat(vacations).OrderBy(e => e.StartDate);

            return entries;
        }

        public IEnumerable<EntryToDisplay> GetAllAcceptedEntries()
        {
            var leaves = db.Leaves
                    .Where(l => l.IsApproved == true)
                    .ToList()
                    .Select(l => new EntryToDisplay
                    {
                        Id = l.Id,
                        StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                        EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                        Comment = l.Comment,
                        EntryType = EntryType.Leave,
                        EntryTypeName = "Zwolnienie",
                        UserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                        SubstituteUserDisplayName = string.Format("{0} {1}", l.SubstituteUser.FirstName, l.SubstituteUser.LastName)
                    });

            var vacations = db.Vacations
                                .Where(v => v.IsApproved == true)
                                .ToList()
                                .Select(v => new EntryToDisplay
                                {
                                    Id = v.Id,
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    Comment = v.Comment,
                                    EntryType = EntryType.Vacation,
                                    EntryTypeName = "Urlop",
                                    UserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", v.SubstituteUser.FirstName, v.SubstituteUser.LastName)
                                });

            var entries = leaves.Concat(vacations).OrderByDescending(e => e.StartDate);

            return entries;
        }

        public IEnumerable<EntryToDisplay> GetAllRejectedEntries()
        {
            var leaves = db.Leaves
                    .Where(l => l.IsApproved == false)
                    .ToList()
                    .Select(l => new EntryToDisplay
                    {
                        Id = l.Id,
                        StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                        EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                        Comment = l.Comment,
                        EntryType = EntryType.Leave,
                        EntryTypeName = "Zwolnienie",
                        UserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                        SubstituteUserDisplayName = string.Format("{0} {1}", l.SubstituteUser.FirstName, l.SubstituteUser.LastName)
                    });

            var vacations = db.Vacations
                                .Where(v => v.IsApproved == false)
                                .ToList()
                                .Select(v => new EntryToDisplay
                                {
                                    Id = v.Id,
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    Comment = v.Comment,
                                    EntryType = EntryType.Vacation,
                                    EntryTypeName = "Urlop",
                                    UserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", v.SubstituteUser.FirstName, v.SubstituteUser.LastName)
                                });

            var entries = leaves.Concat(vacations).OrderByDescending(e => e.StartDate);

            return entries;
        }

        public void AcceptLeave(int leaveId)
        {
            var leave = db.Leaves.FirstOrDefault(l => l.Id == leaveId);

            if (leave == null)
            {
                throw new Exception();
            }

            leave.IsApproved = true;

            db.SaveChanges();
        }

        public void RejectLeave(int leaveId)
        {
            var leave = db.Leaves.FirstOrDefault(l => l.Id == leaveId);

            if (leave == null)
            {
                throw new Exception();
            }

            leave.IsApproved = false;

            db.SaveChanges();
        }

        public void AcceptVacation(int vacationId)
        {
            var vacation = db.Vacations.FirstOrDefault(v => v.Id == vacationId);

            if (vacation == null)
            {
                throw new Exception();
            }

            vacation.IsApproved = true;

            db.SaveChanges();
        }

        public void RejectVacation(int vacationId)
        {
            var vacation = db.Vacations.FirstOrDefault(v => v.Id == vacationId);

            if (vacation == null)
            {
                throw new Exception();
            }

            vacation.IsApproved = false;

            db.SaveChanges();
        }

        public IEnumerable<EntryToRaport> GetEntriesToRaportForCurrentYear()
        {
            var leaves = db.Leaves
                            .Where(l => l.StartDate.Year == DateTime.Now.Year
                            || l.EndDate.Year == DateTime.Now.Year)
                            .ToList()
                            .Select(l => new EntryToRaport
                            {
                                UserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                                SubstituteUserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                                StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                                EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                                IsApproved = l.IsApproved != null ? (l.IsApproved == true ? "Tak" : "Nie") : "Oczekuje",
                                EntryType = string.Format("Zwolnienie ({0})", l.Type.Name)
                            });

            var vacations = db.Vacations
                                .Where(v => v.StartDate.Year == DateTime.Now.Year
                                || v.EndDate.Year == DateTime.Now.Year)
                                .ToList()
                                .Select(v => new EntryToRaport
                                {
                                    UserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : "Oczekuje",
                                    EntryType = string.Format("Urlop ({0})", v.Type.Name)
                                });

            var entries = leaves.Concat(vacations).OrderBy(e => e.StartDate);

            return entries;
        }

        public IEnumerable<EntryToRaport> GetEntriesToRaportForTimeRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new EntryException("Wybrano niewłaściwy zakres dat.");
            }
            var leaves = db.Leaves
                            .Where(l => l.StartDate < endDate && l.EndDate > startDate)
                            .ToList()
                            .Select(l => new EntryToRaport
                            {
                                UserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                                SubstituteUserDisplayName = string.Format("{0} {1}", l.User.FirstName, l.User.LastName),
                                StartDate = l.StartDate.ToString("dd.MM.yyyy"),
                                EndDate = l.EndDate.ToString("dd.MM.yyyy"),
                                IsApproved = l.IsApproved != null ? (l.IsApproved == true ? "Tak" : "Nie") : "Oczekuje",
                                EntryType = string.Format("Zwolnienie ({0})", l.Type.Name)
                            });

            var vacations = db.Vacations
                                .Where(v => v.StartDate < endDate && v.EndDate > startDate)
                                .ToList()
                                .Select(v => new EntryToRaport
                                {
                                    UserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    SubstituteUserDisplayName = string.Format("{0} {1}", v.User.FirstName, v.User.LastName),
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : "Oczekuje",
                                    EntryType = string.Format("Urlop ({0})", v.Type.Name)
                                });

            var entries = leaves.Concat(vacations).OrderBy(e => e.StartDate);

            return entries;
        }
    }
}
