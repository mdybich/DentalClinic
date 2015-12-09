using System.Collections.Generic;
using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<BasicUserToDisplay> GetBasicUsers();
    }
}
