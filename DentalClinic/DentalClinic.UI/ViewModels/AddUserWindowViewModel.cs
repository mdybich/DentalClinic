using DentalClinic.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using DentalClinic.UI.Interfaces;
using DentalClinic.Services.Services;
using System.Collections.ObjectModel;
using DentalClinic.Services.Helpers;
using GalaSoft.MvvmLight.Messaging;
using DentalClinic.Services.Exceptions;
using System.Windows;

namespace DentalClinic.UI.ViewModels
{
    public class AddUserWindowViewModel : ViewModelBase
    {

        #region Services Reference
        private readonly IUserService _userService;
        #endregion

        #region Properties For Binding
        private string _login;
        private string _firstName;
        private string _lastName;
        private int? _selectedRoleId;
        private ObservableCollection<RoleToDisplay> _roles;

        public string Login
        {
            get { return this._login; }
            set { Set(() => Login, ref this._login, value); }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { Set(() => FirstName, ref this._firstName, value); }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { Set(() => LastName, ref this._lastName, value); }
        }

        public int? SelectedRoleId
        {
            get { return this._selectedRoleId; }
            set { Set(() => SelectedRoleId, ref this._selectedRoleId, value); }
        }

        public ObservableCollection<RoleToDisplay> Roles
        {
            get { return this._roles; }
            set { Set(() => Roles, ref this._roles, value); }
        }

        public string Password { private get; set; }
        public string PasswordConfirmation { private get; set; }
        #endregion

        #region Commands
        public RelayCommand<IClosable> CancelCommand { get; set; }
        public RelayCommand<IClosable> SaveCommand { get; set; }
        #endregion

        #region Constructors
        public AddUserWindowViewModel() : this(new UserService())
        { }

        public AddUserWindowViewModel(IUserService userService)
        {
            Roles = new ObservableCollection<RoleToDisplay>();
            _userService = userService;
            RetrieveData();
            ButtonAssignment();
        }
        #endregion

        #region Command Executions
        private void CancelExecute(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void SaveExecute(IClosable window)
        {
            var userToAdd = new UserToAdd
            {
                Login = this.Login,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password,
                PasswordConfirmation = this.PasswordConfirmation,
                RoleId = (int)this.SelectedRoleId
            };

            try
            {
                _userService.AddUser(userToAdd);

                Messenger.Default.Send<UserToAdd>(userToAdd);

                if (window != null)
                {
                    window.Close();
                }
            }
            catch(UserException exc)
            {
                MessageBox.Show(exc.Message, "Błąd");
            }
            
        }
        #endregion

        #region Private methods
        private void RetrieveData()
        {
            var roles = _userService.GetRoles();

            foreach (var role in roles)
            {
                Roles.Add(role);
            }

        }
        private void ButtonAssignment()
        {
            this.CancelCommand = new RelayCommand<IClosable>(CancelExecute);
            this.SaveCommand = new RelayCommand<IClosable>(SaveExecute, IsModelValid);
        }

        private bool IsModelValid(IClosable arg)
        {
            return SelectedRoleId != null
                && !string.IsNullOrEmpty(Login)
                && !string.IsNullOrWhiteSpace(Login)
                && !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrEmpty(Password)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrEmpty(PasswordConfirmation)
                && !string.IsNullOrWhiteSpace(PasswordConfirmation);

        }



        #endregion
    }
}
