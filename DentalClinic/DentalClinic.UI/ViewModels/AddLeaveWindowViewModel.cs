using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using DentalClinic.Services.Helpers;
using DentalClinic.UI.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using DentalClinic.Services.Exceptions;
using System.Windows;

namespace DentalClinic.UI.ViewModels
{
    public class AddLeaveWindowViewModel : ViewModelBase
    {
        #region Services Reference
        private readonly ILeaveService _leaveService;
        private readonly IUserService _userService;
        #endregion

        #region Properties For Binding
        private ObservableCollection<LeaveTypeToDisplay> _leavesTypes;
        private ObservableCollection<BasicUserToDisplay> _users;
        private int? _selectedUserId;
        private int? _selectedLeaveTypeId;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _comment;

        public ObservableCollection<LeaveTypeToDisplay> LeavesTypes
        {
            get { return this._leavesTypes; }
            set { Set(() => LeavesTypes, ref this._leavesTypes, value); }
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

        public int? SelectedLeaveTypeId
        {
            get { return this._selectedLeaveTypeId; }
            set { Set(() => SelectedLeaveTypeId, ref this._selectedLeaveTypeId, value); }
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
        public AddLeaveWindowViewModel() : this (new LeaveService(), new UserService())
        {
            _leavesTypes = new ObservableCollection<LeaveTypeToDisplay>();
            _users = new ObservableCollection<BasicUserToDisplay>();

            RetrieveData();
            ButtonAssignment();
        }

        public AddLeaveWindowViewModel(ILeaveService leaveService, IUserService userService)
        {
            _leaveService = leaveService;
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
            var leaveToAdd = new LeaveToAdd
            {
                UserId = (int) SelectedUserId,
                LeaveTypeId = (int) SelectedLeaveTypeId,
                StartDate = (DateTime) StartDate,
                EndDate = (DateTime) EndDate,
                Comment = Comment
            };

            try
            {
                _leaveService.AddLeave(leaveToAdd);

                Messenger.Default.Send<LeaveToAdd>(leaveToAdd);

                if (window != null)
                {
                    window.Close();
                }
            }
            catch(LeaveException exc)
            {
                MessageBox.Show(exc.Message, "Błąd");
            }
        }
        #endregion

        #region Private Methods
        private void RetrieveData()
        {
            var leavesTypes = _leaveService.GetAllLeavesTypes();
            foreach (var leaveType in leavesTypes)
            {
                LeavesTypes.Add(leaveType);
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
                && SelectedLeaveTypeId != null
                && StartDate != null
                && EndDate != null;
        }

        #endregion

    }
}
