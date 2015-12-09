using DentalClinic.Services.Helpers;

namespace DentalClinic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserInfo AuthenticateUser(string userName, string password);
    }
}
