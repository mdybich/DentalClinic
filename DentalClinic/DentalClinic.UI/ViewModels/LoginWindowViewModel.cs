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
        private string _userPassword;

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

        public string UserPassword
        {
            get { return this._userPassword; }
            set { Set(() => UserPassword, ref this._userPassword, value); }
        }

        #region Commands

        public RelayCommand<IClosable> LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        #endregion
        private void LoginExecute(IClosable loginWindow)
        {
            try
            {
                var user = _authenticationService.AuthenticateUser(UserLogin, UserPassword);
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
