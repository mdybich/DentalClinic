using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using DentalClinic.UI.Views;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace DentalClinic.UI.ViewModels
{
    public class RegistrantWindowViewModel : LoggedBaseViewModel
    {
        #region Services Reference
        private readonly ILeaveService _leaveService;
        private readonly IVacationService _vacationService;
        #endregion

        private ObservableCollection<LeaveToDisplay> _leaves;
        private ObservableCollection<VacationToDisplay> _vacations; 

        #region Commands
        public RelayCommand OpenAddLeaveWindowCommand { get; set; }
        public RelayCommand OpenAddVacationWindowCommand  { get; set; }
        #endregion

        public ObservableCollection<LeaveToDisplay> Leaves
        {
            get { return this._leaves; }
            set { Set(() => Leaves, ref this._leaves, value); }
        }

        public ObservableCollection<VacationToDisplay> Vacations
        {
            get { return this._vacations; }
            set { Set(() => Vacations, ref this._vacations, value); }
        }

        public RegistrantWindowViewModel() : this(new LeaveService(), new VacationService())
        {
            CommandAssignment();
            Leaves = new ObservableCollection<LeaveToDisplay>();
            Vacations = new ObservableCollection<VacationToDisplay>();
            RetrieveData();
            Messenger.Default.Register<LeaveToAdd>(this, RefreshLeavesGrid);
            Messenger.Default.Register<VacationToAdd>(this, RefreshVacationsGrid);
        }

        public RegistrantWindowViewModel(ILeaveService leaveService, IVacationService vacationService)
        {
            _leaveService = leaveService;
            _vacationService = vacationService;
        }

        private void RetrieveData()
        {
            RetrieveLeaves();
            RetrieveVacations();
        }

        private void RetrieveLeaves()
        {
            if (Leaves.Count != 0)
            {
                Leaves.Clear();
            }

            var leavesData = _leaveService.GetAllLeaves();

            foreach (var leave in leavesData)
            {
                Leaves.Add(leave);
            }
        }

        public void RetrieveVacations()
        {
            if (Vacations.Count != 0)
            {
                Vacations.Clear();
            }

            var vacationsData = _vacationService.GetAllVacations();

            foreach (var vacation in vacationsData)
            {
                Vacations.Add(vacation);
            }
        }

        private void CommandAssignment()
        {
            this.OpenAddLeaveWindowCommand = new RelayCommand(OpenAddLeaveWindowExecute);
            this.OpenAddVacationWindowCommand = new RelayCommand(OpenAddVacationWindowExecute);
        }

        private void OpenAddLeaveWindowExecute()
        {
            ShowWindow<AddLeaveWindow>();
        }

        private void OpenAddVacationWindowExecute()
        {
            ShowWindow<AddVacationWindow>();
        }

        private void RefreshLeavesGrid(LeaveToAdd newLeave)
        {
            RetrieveLeaves();
        }

        private void RefreshVacationsGrid(VacationToAdd newVacation)
        {
            RetrieveVacations();
        }
    }
}
