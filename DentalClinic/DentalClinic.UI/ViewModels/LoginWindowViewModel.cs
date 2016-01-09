using DentalClinic.Services.Interfaces;
using System;
using DentalClinic.Services.Services;
using DentalClinic.UI.Authorization;
using System.Threading;
using System.Windows;
using DentalClinic.UI.Views;
using GalaSoft.MvvmLight.CommandWpf;
using DentalClinic.UI.Interfaces;

namespace DentalClinic.UI.ViewModels
{
    public class LoginWindowViewModel : BaseViewModel
    {

        private readonly IAuthenticationService _authenticationService;

        private string _userLogin;

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
            get { return this._userLogin; }
            set { Set(() => UserLogin, ref this._userLogin, value); }
        }

        public string Password { get; set; }

        #region Commands

        public RelayCommand<IClosable> LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        #endregion
        private void LoginExecute(IClosable loginWindow)
        {
            try
            {
                var user = _authenticationService.AuthenticateUser(UserLogin, Password);
                var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
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
                    case 2:
                        ShowWindow<WorkerWindow>();
                        break;
                    case 3:
                        ShowWindow<RegistrantWindow>();
                        break;
                    default:
                        throw new ArgumentException("Incorrect Role");
                }

                //close login window

                if (loginWindow != null)
                {
                    loginWindow.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Niepoprawny login bądź hasło");
            }
        }

        private void ButtonAssignment()
        {
            this.LoginCommand = new RelayCommand<IClosable>(LoginExecute);
        }
    }
}
