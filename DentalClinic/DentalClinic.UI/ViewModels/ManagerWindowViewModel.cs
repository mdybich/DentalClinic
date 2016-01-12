using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using DentalClinic.UI.Views;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace DentalClinic.UI.ViewModels
{
    public class ManagerWindowViewModel : LoggedBaseViewModel
    {
        #region Services Reference
        private readonly IEntryService _entryService;
        #endregion

        #region Properties For Binding
        private ObservableCollection<EntryToDisplay> _pendingEntries;
        private ObservableCollection<EntryToDisplay> _acceptedEntries;
        private ObservableCollection<EntryToDisplay> _rejectedEntries;


        public ObservableCollection<EntryToDisplay> PendingEntries
        {
            get { return this._pendingEntries; }
            set { Set(() => PendingEntries, ref this._pendingEntries, value); }
        }

        public ObservableCollection<EntryToDisplay> AcceptedEntries
        {
            get { return this._acceptedEntries; }
            set { Set(() => AcceptedEntries, ref this._acceptedEntries, value); }
        }

        public ObservableCollection<EntryToDisplay> RejectedEntries
        {
            get { return this._rejectedEntries; }
            set { Set(() => RejectedEntries, ref this._rejectedEntries, value); }
        }
        #endregion

        #region Commands
        public RelayCommand<EntryToDisplay> AcceptEntryCommand { get; set; }
        public RelayCommand<EntryToDisplay> RejectEntryCommand { get; set; }
        public RelayCommand GenerateRaportCommand { get; set; }
        #endregion

        #region Constructors
        public ManagerWindowViewModel() : this(new EntryService())
        {
            PendingEntries = new ObservableCollection<EntryToDisplay>();
            AcceptedEntries = new ObservableCollection<EntryToDisplay>();
            RejectedEntries = new ObservableCollection<EntryToDisplay>();

            RetrieveData();

            ButtonAsignment();
        }

        public ManagerWindowViewModel(IEntryService entryService)
        {
            _entryService = entryService;
        }
        #endregion

        #region Private Methods
        private void RetrieveData()
        {
            if (PendingEntries.Count != 0)
            {
                PendingEntries.Clear();
            }

            var pendingEntries = _entryService.GetAllPendingEntries();

            foreach (var pendingEntry in pendingEntries)
            {
                PendingEntries.Add(pendingEntry);
            }

            if (AcceptedEntries.Count != 0)
            {
                AcceptedEntries.Clear();
            }

            var acceptedEntries = _entryService.GetAllAcceptedEntries();

            foreach (var acceptedEntry in acceptedEntries)
            {
                AcceptedEntries.Add(acceptedEntry);
            }

            if (RejectedEntries.Count != 0)
            {
                RejectedEntries.Clear();
            }

            var rejectedEntries = _entryService.GetAllRejectedEntries();

            foreach (var rejectedEntry in rejectedEntries)
            {
                RejectedEntries.Add(rejectedEntry);
            }
        }

        private void ButtonAsignment()
        {
            this.AcceptEntryCommand = new RelayCommand<EntryToDisplay>(AcceptEntryExecute);
            this.RejectEntryCommand = new RelayCommand<EntryToDisplay>(RejectEntryExecute);
            this.GenerateRaportCommand = new RelayCommand(GenerateRaportExecute);
        }

        private void AcceptEntryExecute(EntryToDisplay entry)
        {
            switch (entry.EntryType)
            {
                case EntryType.Leave:
                    _entryService.AcceptLeave(entry.Id);
                    break;
                case EntryType.Vacation:
                    _entryService.AcceptVacation(entry.Id);
                    break;
            }

            RetrieveData();
        }

        private void RejectEntryExecute(EntryToDisplay entry)
        {
            switch (entry.EntryType)
            {
                case EntryType.Leave:
                    _entryService.RejectLeave(entry.Id);
                    break;
                case EntryType.Vacation:
                    _entryService.RejectVacation(entry.Id);
                    break;
            }

            RetrieveData();
        }

        private void GenerateRaportExecute()
        {
            ShowWindow<GenerateRaportWindow>();
        }
        #endregion
    }
}
