using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Transactions;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _Context;
        public TransactionRepository(AppDbContext context)
        {
            _Context = context;
        }

      
        public async Task<Transaction?> GetByIdAsync(int Id)
        {
            return await _Context.Transactions
                .Include(x => x.Entries)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Person)
                .FirstOrDefaultAsync(x=>x.Id ==Id);

        }
        
        public async Task<int?> AddAsync(TransactionType Type, decimal Amount)
        {
            var NewTransaction = new Transaction()
            {
                Type = Type,
                Amount = Amount,
                Date = DateTime.Now
            };
            await _Context.Transactions.AddAsync(NewTransaction);

            return await _Context.SaveChangesAsync() != 0
                   ? NewTransaction.Id
                   : null;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransfersAsync(GetTransferDTO dto)
        {
            var Query = FilterUsingAccountNumberAndTransactionType(dto.AccountNumber, TransactionType.Transfer);

            if (dto.EntryType.HasValue)
            {
                Query = Query
                   .Where(x => x.Entries.Any(e => e.EntryType == dto.EntryType && e.Account.Number == dto.AccountNumber));
            }

            return await Query
                .Include(x => x.Entries)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Person)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllDepositsAsync(string? AccountNumber)
        {
            
            if (AccountNumber == null)
            {

            }
            var Query = FilterUsingAccountNumberAndTransactionType(AccountNumber, TransactionType.Deposit);

            return await Query
                .Include(x => x.Entries)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Person)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllWithdrawsAsync(string? AccountNumber)
        {
          var Query =  FilterUsingAccountNumberAndTransactionType(AccountNumber, TransactionType.Withdrawal);

            return await Query
                .Include(x => x.Entries)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Person)
                .ToListAsync();
        }


        private IQueryable<Transaction> FilterUsingAccountNumberAndTransactionType
            (string? AccountNumber, TransactionType TransactionType)
        {
            if (AccountNumber == null)
            {
                return _Context
                .Transactions
                .Where(x =>
                x.Entries.Where(e => x.Type == TransactionType).Any());
            }

            return _Context
                .Transactions
                .Where(x =>
                x.Entries.Where(e =>
                e.Account.Number == AccountNumber && x.Type == TransactionType).Any());
        }
        
    }

}
