using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Interfaces.TransactionEntries
{
    public interface ITransactionEntryRepository
    {
        public Task<int> AddTransactionEntryAsync(TransactionEntry Entry);

        public Task<TransactionEntry?> GetByIdAsync(int Id);

        public Task<IEnumerable<TransactionEntry>> GetAllAsync();

        public Task<IEnumerable<TransactionEntry>> GetAllByAccountIdAsync(int AccountId);

        public Task<IEnumerable<TransactionEntry>> GetAllByAccountIdAndTransactionTypeAsync(int AccountId, TransactionType transactionType);
    }
}
