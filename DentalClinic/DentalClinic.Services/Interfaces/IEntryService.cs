using DentalClinic.Services.Helpers;
using System;
using System.Collections.Generic;

namespace DentalClinic.Services.Interfaces
{
    public interface IEntryService
    {
        IEnumerable<EntryToDisplay> GetAllPendingEntries();
        IEnumerable<EntryToDisplay> GetAllAcceptedEntries();
        IEnumerable<EntryToDisplay> GetAllRejectedEntries();
        void AcceptLeave(int leaveId);
        void RejectLeave(int leaveId);
        void AcceptVacation(int vacationId);
        void RejectVacation(int vacationId);
        IEnumerable<EntryToRaport> GetEntriesToRaportForCurrentYear();
        IEnumerable<EntryToRaport> GetEntriesToRaportForTimeRange(DateTime startDate, DateTime endDate);
    }
}
