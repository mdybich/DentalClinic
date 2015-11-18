using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.UI.ViewModels
{
    public class RegistrantWindowViewModel : LoggedBaseViewModel
    {
        private readonly ILeaveService _leaveService;

        private ObservableCollection<LeaveToDisplay> leaves;

        public ObservableCollection<LeaveToDisplay> Leaves
        {
            get { return this.leaves; }
            set
            {
                this.leaves = value;
                OnPropertyChanged("Leaves");
            }
        }

        public RegistrantWindowViewModel() : this(new LeaveService())
        {
            Leaves = new ObservableCollection<LeaveToDisplay>();
            RetrieveData();
        }

        public RegistrantWindowViewModel(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        private void RetrieveData()
        {
            var leavesData = _leaveService.GetAll();

            foreach (var leave in leavesData)
            {
                Leaves.Add(leave);
            }
        }
    }
}
