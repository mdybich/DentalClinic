using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DentalClinic.UI.Authorization;

namespace DentalClinic.UI.ViewModels
{
    public class WorkerWindowViewModel : LoggedBaseViewModel
    {
        #region Services Reference
        private readonly ILeaveService _leaveService;
        private readonly IVacationService _vacationService;
        #endregion

        private ObservableCollection<LeaveToDisplay> _leaves;
        private ObservableCollection<VacationToDisplay> _vacations;

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

        public WorkerWindowViewModel() : this(new LeaveService(), new VacationService())
        {
            Leaves = new ObservableCollection<LeaveToDisplay>();
            Vacations = new ObservableCollection<VacationToDisplay>();
            RetrieveData();
        }

        public WorkerWindowViewModel(ILeaveService leaveService, IVacationService vacationService)
        {
            _leaveService = leaveService;
            _vacationService = vacationService;
        }

        private void RetrieveData()
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            var leavesData = _leaveService.GetLeavesByLogin(customPrincipal.Identity.Name);

            foreach (var leave in leavesData)
            {
                Leaves.Add(leave);
            }

            var vacationsData = _vacationService.GetVacationsByLogin(customPrincipal.Identity.Name);

            foreach (var vacation in vacationsData)
            {
                Vacations.Add(vacation);
            }
        }
    }
}
