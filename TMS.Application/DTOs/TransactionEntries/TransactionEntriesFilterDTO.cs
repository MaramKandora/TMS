using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionEntriesFilterDTO
    {
        public required int AccountId { get; set; }
        public required TransactionType TransactionType { get; set; } ;
    }
}
