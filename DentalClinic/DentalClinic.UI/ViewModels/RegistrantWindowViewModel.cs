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
        #endregion

        private ObservableCollection<LeaveToDisplay> _leaves;

        #region Commands
        public RelayCommand OpenAddLeaveWindowCommand { get; set; }
        #endregion

        public ObservableCollection<LeaveToDisplay> Leaves
        {
            get { return this._leaves; }
            set { Set(() => Leaves, ref this._leaves, value); }
        }

        public RegistrantWindowViewModel() : this(new LeaveService())
        {
            CommandAssignment();
            Leaves = new ObservableCollection<LeaveToDisplay>();
            RetrieveData();
            Messenger.Default.Register<LeaveToDisplay>(this, AddNewLeaveToGrid);
        }

        public RegistrantWindowViewModel(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        private void RetrieveData()
        {
            var leavesData = _leaveService.GetAllLeaves();

            foreach (var leave in leavesData)
            {
                Leaves.Add(leave);
            }
        }

        private void CommandAssignment()
        {
            this.OpenAddLeaveWindowCommand = new RelayCommand(OpenAddLeaveWindowExecute);
        }

        private void OpenAddLeaveWindowExecute()
        {
            ShowWindow<AddLeaveWindow>();
        }

        private void AddNewLeaveToGrid(LeaveToDisplay newLeave)
        {
            Leaves.Add(newLeave);
        }
    }
}
