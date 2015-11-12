using DentalClinic.Services.Interfaces;
using System;
using System.Windows.Input;
using DentalClinic.Services.Services;
using DentalClinic.UI.Authorization;
using System.Threading;
using System.Windows;
using DentalClinic.UI.Views;

namespace DentalClinic.UI.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {

        private readonly IAuthenticationService _authenticationService;
        private string userLogin;
        private string userPassword;

        public LoginWindowViewModel() : this(new AuthenticationService())
        {
            ButtonAssignment();
        }

        public LoginWindowViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public string UserLogin
        {
            get { return this.userLogin; }
            set
            {
                this.userLogin = value;
                this.OnPropertyChanged("User Login");
            }
        }

        public string UserPassword
        {
            get { return this.userPassword; }
            set
            {
                this.userPassword = value;
                this.OnPropertyChanged("User Password");
            }
        }

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        #endregion
        private void LoginExecute()
        {
            try
            {
                var user = _authenticationService.AuthenticateUser(UserLogin, UserPassword);
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Login, user.FirstName, user.LastName, user.Role);

                //display correct window
                switch (user.Role.Id)
                {
                    case 1:
                        ShowWindow<AdministratorWindow>();
                        break;
                    case 3:
                        ShowWindow<RegistrantWindow>();
                        break;
                    default:
                        throw new ArgumentException("Incorrect Role");
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("ZLY LOGIN ALBO HASLO");
            }
        }
        private void ButtonAssignment()
        {
            this.LoginCommand = new CommandHelper(LoginExecute);
        }
    }
}
