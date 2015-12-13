using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IVacationService
    {
        IEnumerable<VacationToDisplay> GetAllVacations();
        IEnumerable<VacationToDisplay> GetVacationsByLogin(string login);
        IEnumerable<VacationTypeToDisplay> GetAllVacationsTypes();
        VacationToDisplay AddVacation(VacationToAdd vacation);
    }
}
