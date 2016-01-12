namespace DentalClinic.Services.Helpers
{
    public class EntryToDisplay
    {
        public int Id { get; set; }
        public EntryType EntryType { get; set; }
        public string EntryTypeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Comment { get; set; }
        public string UserDisplayName { get; set; }
        public string SubstituteUserDisplayName { get; set; }
    }
}
