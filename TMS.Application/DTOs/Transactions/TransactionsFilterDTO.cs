using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionsFilterDTO
    {
        public string? AccountNumber { get; set; } = null;
        public TransactionType? TransactionType { get; set; } = null;

       

    }
}
