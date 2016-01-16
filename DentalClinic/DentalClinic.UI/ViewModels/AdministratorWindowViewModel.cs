using System.Collections.ObjectModel;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using GalaSoft.MvvmLight.CommandWpf;
using DentalClinic.Services.Exceptions;
using System.Windows;
using DentalClinic.UI.Views;
using GalaSoft.MvvmLight.Messaging;

namespace DentalClinic.UI.ViewModels
{
    public class AdministratorWindowViewModel : LoggedBaseViewModel
    {
        #region Services Reference
        private readonly IUserService _userService;
        private readonly ILeaveService _leaveService;
        private readonly IVacationService _vacationService;
        #endregion

        #region Properties For Binding
        private ObservableCollection<UserToDisplay> _users;
        private ObservableCollection<VacationTypeToDisplay> _vacationTypes;
        private ObservableCollection<LeaveTypeToDisplay> _leavesTypes;
        private string _newVacationTypeName;
        private string _newLeaveTypeName;


        public ObservableCollection<UserToDisplay> Users
        {
            get { return this._users; }
            set { Set(() => Users, ref this._users, value); }
        }

        public ObservableCollection<VacationTypeToDisplay> VacationTypes
        {
            get { return this._vacationTypes; }
            set { Set(() => VacationTypes, ref this._vacationTypes, value); }
        }

        public ObservableCollection<LeaveTypeToDisplay> LeaveTypes
        {
            get { return this._leavesTypes; }
            set { Set(() => LeaveTypes, ref this._leavesTypes, value); }
        }

        public string NewVacationTypeName
        {
            get { return this._newVacationTypeName; }
            set { Set(() => NewVacationTypeName, ref this._newVacationTypeName, value); }
        }

        public string NewLeaveTypeName
        {
            get { return this._newLeaveTypeName; }
            set { Set(() => NewLeaveTypeName, ref this._newLeaveTypeName, value); }
        }
        #endregion

        #region Commands

        public RelayCommand<UserToDisplay> DeleteUserCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand<VacationTypeToDisplay> DeleteVacationTypeCommand { get; set; }
        public RelayCommand<LeaveTypeToDisplay> DeleteLeaveTypeCommand { get; set; }
        public RelayCommand AddVacationTypeCommand { get; set; }
        public RelayCommand AddLeaveTypeCommand { get; set; }

        #endregion

        #region Constructors
        public AdministratorWindowViewModel() : this (new UserService(), new LeaveService(), new VacationService())
        {
            _users = new ObservableCollection<UserToDisplay>();
            _vacationTypes = new ObservableCollection<VacationTypeToDisplay>();
            _leavesTypes = new ObservableCollection<LeaveTypeToDisplay>();

            RetrieveData();
            ButtonAssignment();
            Messenger.Default.Register<UserToAdd>(this, RefreshUsers);
        }

        public AdministratorWindowViewModel(IUserService userService,
            ILeaveService leaveService, 
            IVacationService vacationService)
        {
            _userService = userService;
            _leaveService = leaveService;
            _vacationService = vacationService;
        }

        #endregion

        #region Command Executions
        #endregion

        #region Private Methods
        private void RetrieveData()
        {
            RetrieveUsers();
            RetrieveVacationTypes();
            RetrieveLeaveTypes();
        }

        private void RetrieveUsers()
        {
            if (Users.Count != 0)
            {
                Users.Clear();
            }

            var users = _userService.GetUsers();

            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private void RetrieveVacationTypes()
        {
            if (VacationTypes.Count != 0)
            {
                VacationTypes.Clear();
            }

            var vacationTypes = _vacationService.GetAllVacationsTypes();

            foreach (var vacationType in vacationTypes)
            {
                VacationTypes.Add(vacationType);
            }
        }

        private void RetrieveLeaveTypes()
        {
            if (LeaveTypes.Count != 0)
            {
                LeaveTypes.Clear();
            }

            var leaveTypes = _leaveService.GetAllLeavesTypes();

            foreach (var leaveType in leaveTypes)
            {
                LeaveTypes.Add(leaveType);
            }
        }

        private void ButtonAssignment()
        {
            this.DeleteUserCommand = new RelayCommand<UserToDisplay>(DeleteUserExecute);
            this.AddUserCommand = new RelayCommand(AddUserExecute);
            this.DeleteVacationTypeCommand = new RelayCommand<VacationTypeToDisplay>(DeleteVacationTypeExecute);
            this.DeleteLeaveTypeCommand = new RelayCommand<LeaveTypeToDisplay>(DeleteLeaveTypeExecute);
            this.AddLeaveTypeCommand = new RelayCommand(AddLeaveTypeExecute);
            this.AddVacationTypeCommand = new RelayCommand(AddVacationTypeExecute);
        }

        private void DeleteUserExecute(UserToDisplay user)
        {
            _userService.DeleteUser(user.Login);
            RetrieveUsers();
        }

        private void AddUserExecute()
        {
            ShowWindow<AddUserWindow>();
        }

        private void DeleteVacationTypeExecute(VacationTypeToDisplay vacationType)
        {
            _vacationService.DeleteVacationType(vacationType.Id);
            RetrieveVacationTypes();
        }

        private void DeleteLeaveTypeExecute(LeaveTypeToDisplay leaveType)
        {
            _leaveService.DeleteLeaveType(leaveType.Id);
            RetrieveLeaveTypes();
        }

        private void AddVacationTypeExecute()
        {
            try
            {
                _vacationService.AddVacationType(NewVacationTypeName);
                NewVacationTypeName = string.Empty;
                CustomMessageBox.CustomMessageBox.Show("Poprawnie dodano nowy rodzaj urlopu.");
            }
            catch(VacationTypeException exc)
            {
                CustomMessageBox.CustomMessageBox.Show(exc.Message, "Błąd");
            }

            
            RetrieveVacationTypes();           
        }

        private void AddLeaveTypeExecute()
        {
            try
            {
                _leaveService.AddLeaveType(NewLeaveTypeName);
                NewLeaveTypeName = string.Empty;
                CustomMessageBox.CustomMessageBox.Show("Poprawnie dodano nowy rodzaj zwolnienia.");
            }
            catch(LeaveTypeException exc)
            {
                CustomMessageBox.CustomMessageBox.Show(exc.Message, "Błąd");
            }
           
            RetrieveLeaveTypes();            
        }

        private void RefreshUsers(UserToAdd u)
        {
            RetrieveUsers();
        }

        #endregion
    }
}
