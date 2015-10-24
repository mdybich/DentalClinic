using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserInfo AuthenticateUser(string userName, string password);
    }
}
