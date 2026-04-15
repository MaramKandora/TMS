using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Enums.TransactionEntries;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionEntryDTO
    {
        public int Id { get; set; }
        public EntryType EntryType { get; set; }

        public int AccountID { get; set; }
        public int TransactionID { get; set; }

    }
}
