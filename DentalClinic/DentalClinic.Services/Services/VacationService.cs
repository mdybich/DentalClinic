using System;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data;
using DentalClinic.Data.Models;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;

namespace DentalClinic.Services.Services
{
    public class VacationService : IVacationService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public IEnumerable<VacationToDisplay> GetAllVacations()
        {
            var vacations = db.Vacations.ToList().Select(v => new VacationToDisplay
            {
                UserFullName = v.User.FirstName + " " + v.User.LastName,
                StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                Comment = v.Comment,
                VacationTypeName = v.Type.Name,
                SubstituteUserFullName = v.SubstituteUser.FirstName + " " + v.SubstituteUser.LastName,
                IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : string.Empty
            });

            return vacations;
        }

        public IEnumerable<VacationToDisplay> GetVacationsByLogin(string login)
        {
            var vacations = db.Vacations.Where(v => v.User.Login == login).ToList().Select(v => new VacationToDisplay
            {
                UserFullName = v.User.FirstName + " " + v.User.LastName,
                StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                Comment = v.Comment,
                VacationTypeName = v.Type.Name,
                SubstituteUserFullName = v.SubstituteUser.FirstName + " " + v.SubstituteUser.LastName,
                IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : string.Empty
            });

            return vacations;
        }

        public IEnumerable<VacationTypeToDisplay> GetAllVacationsTypes()
        {
            var vacationtypes = db.VacationTypes.Select(v => new VacationTypeToDisplay
            {
                Id = v.Id,
                Name = v.Name
            });

            return vacationtypes;
        }

        public VacationToDisplay AddVacation(VacationToAdd vacationToAdd)
        {
            var substituteUsersId = db.Users.Where(u => u.Id != vacationToAdd.UserId).Select(u => u.Id).FirstOrDefault();

            var vacation = new Vacation
            {
                UserId = vacationToAdd.UserId,
                VacationTypeId = vacationToAdd.VacationTypeId,
                SubstituteUserId = substituteUsersId,
                StartDate = vacationToAdd.StartDate,
                EndDate = vacationToAdd.EndDate,
                Comment = vacationToAdd.Comment,
            };

            db.Vacations.Add(vacation);
            db.SaveChanges();

            var newLeave = db.Vacations.Include("User").Include("Type").Include("SubstituteUser")
                .Where(v => v.Id == vacation.Id).ToList().Select(v => new VacationToDisplay
                {
                    UserFullName = $"{v.User.FirstName} {v.User.LastName}",
                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                    Comment = v.Comment,
                    VacationTypeName = v.Type.Name,
                    SubstituteUserFullName = $"{v.SubstituteUser.FirstName} {v.SubstituteUser.LastName}",
                    IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : string.Empty
                }).FirstOrDefault();

            return newLeave;
        }
    }
}
