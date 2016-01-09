using System;
using System.Collections.Generic;
using System.Linq;
using DentalClinic.Data;
using DentalClinic.Data.Models;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Exceptions;

namespace DentalClinic.Services.Services
{
    public class VacationService : IVacationService
    {
        private DentalClinicContext db = new DentalClinicContext();

        public IEnumerable<VacationToDisplay> GetAllVacations()
        {
            var vacations = db.Vacations
                                .ToList()
                                .Select(v => new VacationToDisplay
                                {
                                    UserFullName = v.User.FirstName + " " + v.User.LastName,
                                    StartDate = v.StartDate.ToString("dd.MM.yyyy"),
                                    EndDate = v.EndDate.ToString("dd.MM.yyyy"),
                                    Comment = v.Comment,
                                    VacationTypeName = v.Type.Name,
                                    SubstituteUserFullName = v.SubstituteUser.FirstName + " " + v.SubstituteUser.LastName,
                                    IsApproved = v.IsApproved != null ? (v.IsApproved == true ? "Tak" : "Nie") : "Oczekuje"
                                });

            return vacations;
        }

        public IEnumerable<VacationToDisplay> GetVacationsByLogin(string login)
        {
            var vacations = db.Vacations
                                .Where(v => v.User.Login == login)
                                .ToList()
                                .Select(v => new VacationToDisplay
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
            var vacationtypes = db.VacationTypes
                                    .Where(v => v.IsActive)
                                    .Select(v => new VacationTypeToDisplay
                                    {
                                        Id = v.Id,
                                        Name = v.Name
                                    });

            return vacationtypes;
        }

        public void AddVacation(VacationToAdd vacationToAdd)
        {
            var isUserAlreadyHaveVacation = db.Vacations
                                                .Any(v => v.StartDate < vacationToAdd.EndDate
                                                && v.EndDate > vacationToAdd.StartDate
                                                && v.UserId == vacationToAdd.UserId);

            if (isUserAlreadyHaveVacation)
            {
                var userDisplayName = db.Users
                                        .Where(u => u.Id == vacationToAdd.UserId)
                                        .ToList()
                                        .Select(u => string.Format("{0} {1}", u.FirstName, u.LastName))
                                        .SingleOrDefault();

                var message = string.Format("{0} w podanym terminie przebywa już na urlopie.", userDisplayName);

                throw new VacationException(message);
            }

            var isUserAlreadyHaveSubstitution = db.Vacations
                                                    .Any(v => v.StartDate < vacationToAdd.EndDate
                                                    && v.EndDate > vacationToAdd.StartDate
                                                    && v.SubstituteUserId == vacationToAdd.UserId);

            if (isUserAlreadyHaveSubstitution)
            {
                var userDisplayName = db.Users
                                        .Where(u => u.Id == vacationToAdd.UserId)
                                        .ToList()
                                        .Select(u => string.Format("{0} {1}", u.FirstName, u.LastName))
                                        .SingleOrDefault();

                var message = string.Format("{0} w podanym terminie przebywa na zastępstwie.", userDisplayName);

                throw new VacationException(message);
            }


            var usersIdOnSubstitution = db.Vacations
                                            .Where(v => v.StartDate < vacationToAdd.EndDate
                                            && v.EndDate > vacationToAdd.StartDate
                                            && v.SubstituteUserId != vacationToAdd.UserId)
                                            .Select(v => v.SubstituteUserId);


            var userRoleId = db.Users
                                .Where(u => u.Id == vacationToAdd.UserId)
                                .Select(u => u.RoleId)
                                .SingleOrDefault();

            var substituteUsersId = db.Users
                                        .Where(u => u.Id != vacationToAdd.UserId
                                        &&
                                        u.RoleId == userRoleId
                                        &&
                                        u.IsActive == true
                                        &&
                                        !u.Vacations.Any(v => v.StartDate < vacationToAdd.EndDate
                                                        && v.EndDate > vacationToAdd.StartDate))
                                        .Select(u => u.Id);

            var substituteUserId = substituteUsersId
                                    .Except(usersIdOnSubstitution)
                                    .FirstOrDefault();

            if (substituteUserId == default(int))
            {
                throw new VacationException("W podanym terminie nie można znaleźć zastępstwa.");
            }

            var vacation = new Vacation
            {
                UserId = vacationToAdd.UserId,
                VacationTypeId = vacationToAdd.VacationTypeId,
                SubstituteUserId = substituteUserId,
                StartDate = vacationToAdd.StartDate,
                EndDate = vacationToAdd.EndDate,
                Comment = vacationToAdd.Comment,
            };

            db.Vacations.Add(vacation);
            db.SaveChanges();
        }

        public void DeleteVacationType(int id)
        {
            var vacationType = db.VacationTypes.FirstOrDefault(v => v.Id == id && v.IsActive);

            if (vacationType == null)
            {
                throw new Exception("Nie ma takiego typu urlopu.");
            }

            vacationType.IsActive = false;

            db.SaveChanges();
        }

        public void AddVacationType(string vacationTypeName)
        {
            if (string.IsNullOrEmpty(vacationTypeName) || string.IsNullOrWhiteSpace(vacationTypeName))
            {
                throw new VacationTypeException("Nie można dodać rodzaju urlopu o pustej nazwie.");
            }

            if (db.VacationTypes.FirstOrDefault(v => v.Name.ToLower() == vacationTypeName.ToLower()) != null)
            {
                throw new VacationTypeException("Podany rodzaj urlopu już istnieje.");
            }

            var vacationTypeToAdd = new VacationType();
            vacationTypeToAdd.Name = vacationTypeName;
            vacationTypeToAdd.IsActive = true;

            db.VacationTypes.Add(vacationTypeToAdd);

            db.SaveChanges();
        }
    }
}
