using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.TransactionEntries
{
    public class TransactionEntryRepository :ITransactionEntryRepository
    {
        private readonly AppDbContext _Context;
        public TransactionEntryRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<int> AddTransactionEntryAsync(TransactionEntry transactionEntry)
        {
            await _Context.TransactionEntries.AddAsync(transactionEntry);
            return transactionEntry.Id;    
        }

        public async Task<IEnumerable<TransactionEntry>> GetAllAsync()
        {
            return await _Context.TransactionEntries.ToListAsync();
        }

        public async Task<IEnumerable<TransactionEntry>> GetAllByAccountIdAsync(int AccountId)
        {
            return await _Context.TransactionEntries.Where(x => x.AccountId == AccountId).ToListAsync();
        }

        public async Task<IEnumerable<TransactionEntry>> GetAllByAccountIdAndTransactionTypeAsync(int AccountId, TransactionType transactionType)
        {
            return await _Context.TransactionEntries.Where(x => x.AccountId == AccountId && x.Transaction.Type == transactionType).ToListAsync();
        }

        public async Task<TransactionEntry?> GetByIdAsync(int Id)
        {
            return await _Context.TransactionEntries.FindAsync(Id);
        }
    }
}
