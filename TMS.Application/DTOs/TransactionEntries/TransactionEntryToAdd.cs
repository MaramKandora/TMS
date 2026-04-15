using TMS.Domain.Enums.TransactionEntries;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionEntryToAdd
    {
        public EntryType EntryType { get; set; }

        public int AccountID { get; set; }
        public int TransactionID { get; set; }
    }
}
