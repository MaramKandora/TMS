using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Accounts;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionEntryDTO
    {
        public int EntryId { get; set; }
       
        public string EntryType { get; set; } = string.Empty;

        public string AccountNumber { get; set; } = null!;

        public string PersonFullName { get; set; } = string.Empty;
        
       

    }
}
