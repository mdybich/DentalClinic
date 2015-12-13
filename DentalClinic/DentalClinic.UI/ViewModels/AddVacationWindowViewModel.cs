using System;
using System.Collections.ObjectModel;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using DentalClinic.UI.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace DentalClinic.UI.ViewModels
{
    public class AddVacationWindowViewModel: ViewModelBase
    {
        #region Services Reference
        private readonly IVacationService _vacationService;
        private readonly IUserService _userService;
        #endregion

        #region Properties For Binding
        private ObservableCollection<VacationTypeToDisplay> _vacationsTypes;
        private ObservableCollection<BasicUserToDisplay> _users;
        private int? _selectedUserId;
        private int? _selectedVacationTypeId;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _comment;

        public ObservableCollection<VacationTypeToDisplay> VacationTypes
        {
            get { return this._vacationsTypes; }
            set { Set(() => VacationTypes, ref this._vacationsTypes, value); }
        }

        public ObservableCollection<BasicUserToDisplay> Users
        {
            get { return this._users; }
            set { Set(() => Users, ref this._users, value); }
        }

        public int? SelectedUserId
        {
            get { return this._selectedUserId; }
            set { Set(() => SelectedUserId, ref this._selectedUserId, value); }
        }

        public int? SelectedVacationTypeId
        {
            get { return this._selectedVacationTypeId; }
            set { Set(() => SelectedVacationTypeId, ref this._selectedVacationTypeId, value); }
        }

        public DateTime? StartDate
        {
            get { return this._startDate; }
            set { Set(() => StartDate, ref this._startDate, value); }
        }

        public DateTime? EndDate
        {
            get { return this._endDate; }
            set { Set(() => EndDate, ref this._endDate, value); }
        }

        public string Comment
        {
            get { return this._comment; }
            set { Set(() => Comment, ref this._comment, value); }
        }
        #endregion

        #region Commands
        public RelayCommand<IClosable> CancelCommand { get; set; }
        public RelayCommand<IClosable> SaveCommand { get; set; }
        #endregion

        #region Constructors
        public AddVacationWindowViewModel() : this (new VacationService(), new UserService())
        {
            _vacationsTypes = new ObservableCollection<VacationTypeToDisplay>();
            _users = new ObservableCollection<BasicUserToDisplay>();

            RetrieveData();
            ButtonAssignment();
        }

        public AddVacationWindowViewModel(IVacationService vacationService, IUserService userService)
        {
            _vacationService = vacationService;
            _userService = userService;
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
            var vacationToAdd = new VacationToAdd
            {
                UserId = (int)SelectedUserId,
                VacationTypeId = (int)SelectedVacationTypeId,
                StartDate = (DateTime)StartDate,
                EndDate = (DateTime)EndDate,
                Comment = Comment
            };

            var newVacationFromDb = _vacationService.AddVacation(vacationToAdd);

            Messenger.Default.Send<VacationToDisplay>(newVacationFromDb);

            if (window != null)
            {
                window.Close();
            }
        }
        #endregion

        #region Private Methods
        private void RetrieveData()
        {
            var vacationsTypes = _vacationService.GetAllVacationsTypes();
            foreach (var vacationsType in vacationsTypes)
            {
                VacationTypes.Add(vacationsType);
            }

            var users = _userService.GetBasicUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private void ButtonAssignment()
        {
            this.CancelCommand = new RelayCommand<IClosable>(CancelExecute);
            this.SaveCommand = new RelayCommand<IClosable>(SaveExecute, IsModelValid);
        }

        private bool IsModelValid(IClosable arg)
        {
            return SelectedUserId != null
                && SelectedVacationTypeId != null
                && StartDate != null
                && EndDate != null;
        }

        #endregion
    }
}
