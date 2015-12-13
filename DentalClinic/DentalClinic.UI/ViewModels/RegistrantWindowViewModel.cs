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
            Messenger.Default.Register<LeaveToDisplay>(this, AddNewLeaveToGrid);
            Messenger.Default.Register<VacationToDisplay>(this, AddNewVacationToGrid);
        }

        public RegistrantWindowViewModel(ILeaveService leaveService, IVacationService vacationService)
        {
            _leaveService = leaveService;
            _vacationService = vacationService;
        }

        private void RetrieveData()
        {
            var leavesData = _leaveService.GetAllLeaves();

            foreach (var leave in leavesData)
            {
                Leaves.Add(leave);
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

        private void AddNewLeaveToGrid(LeaveToDisplay newLeave)
        {
            Leaves.Add(newLeave);
        }

        private void AddNewVacationToGrid(VacationToDisplay newVacation)
        {
            Vacations.Add(newVacation);
        }
    }
}
