using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Enums.TransactionEntries;

namespace TMS.Application.DTOs.Transactions
{
    public class GetTransferDTO
    {
        public required string AccountNumber { get; set; }
        public EntryType? EntryType { get; set; } = null;
    }
}
