using System.Collections.Generic;
using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IVacationService
    {
        IEnumerable<VacationToDisplay> GetAllVacations();
        IEnumerable<VacationToDisplay> GetVacationsByLogin(string login);
        IEnumerable<VacationTypeToDisplay> GetAllVacationsTypes();
        void AddVacation(VacationToAdd vacation);
        void DeleteVacationType(int id);
        void AddVacationType(string vacationTypeName);
    }
}
