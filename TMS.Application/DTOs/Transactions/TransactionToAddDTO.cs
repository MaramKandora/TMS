using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.DTOs.Transactions
{
    public class TransactionToAddDTO
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }

        public ICollection<TransactionEntry> Entries { get; set; } = new List<TransactionEntry>();
    }
}
