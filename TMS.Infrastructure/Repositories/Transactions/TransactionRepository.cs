using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<int> AddAsync(Transaction transaction)
        {
            await _Context.Transactions.AddAsync(transaction);
           await _Context.SaveChangesAsync();
            return transaction.Id;

        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _Context.Transactions.ToListAsync();
        }


        public async Task<Transaction?> GetByIdAsync(int Id)
        {
            return await _Context.Transactions.FindAsync(Id);

        }

        public async Task<bool> TransferAsync(string FromAccountNumber, string ToAccountNumber, decimal Amount)
        {
            
            using var transaction = await _Context.Database.BeginTransactionAsync();

            try
            {
                var FromAccount = await _GetAccountAsync(FromAccountNumber);
                if (FromAccount is null)
                {
                    await transaction.RollbackAsync();
                    return false;// Error invalid FromAccountNumber
                }
                
                var ToAccount = await _GetAccountAsync(ToAccountNumber);
                if (ToAccount is null)
                {
                    await transaction.RollbackAsync();
                    return false;// Error invalid ToAccountNumber
                }

                if (await _WithdrawalHelperAsync(FromAccount, Amount))
                {
                    await transaction.RollbackAsync();
                    return false;// Error insufficient Balance
                }

                await _DepositHelperAsync(ToAccount, Amount);


                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> WithdrawAsync(string AccountNumber, decimal Amount)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();

            try
            {
               
                var Account = await _GetAccountAsync(AccountNumber);
                if (Account is null)
                {
                    await transaction.RollbackAsync();
                    return false;// Error invalid accountNumber
                }

                if (await _WithdrawalHelperAsync(Account, Amount))
                {
                    await transaction.RollbackAsync();
                    return false;// Error insufficient Balance
                }

                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> DepositAsync(string AccountNumber, decimal Amount)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();

            try
            {
                var Account = await _GetAccountAsync(AccountNumber);
                if (Account is null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                await _DepositHelperAsync(Account, Amount);
              
                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
               await transaction.RollbackAsync();
                return false;
            }

            return true;
        }

        //TODO: we should replace this func with the one exists in AccountRepo
        private async Task<Account?> _GetAccountAsync(string AccountNumber)
        {
            var Account = await _Context.Set<Account>().SingleOrDefaultAsync(x => x.Number == AccountNumber);

            return Account;
        }
        private async Task _DepositHelperAsync(Account Account, decimal Amount)
        {
            Account.Balance += Amount;

            var NewTransaction = new Transaction()
            {
                Type = TransactionType.Deposit,
                Amount = Amount,
                Date = DateTime.Now
            };
            await _Context.Transactions.AddAsync(NewTransaction);

            var NewEntry = new TransactionEntry()
            {
                EntryType = EntryType.In,
                TransactionId = NewTransaction.Id,
                AccountId = Account.Id
            };
            await _Context.TransactionEntries.AddAsync(NewEntry);

        }

        private async Task<bool> _WithdrawalHelperAsync(Account Account, decimal Amount)
        {
            if (Account.Balance < Amount)
                return false;

            Account.Balance -= Amount;

            var NewTransaction = new Transaction()
            {
                Type = TransactionType.Withdrawal,
                Amount = Amount,
                Date = DateTime.Now
            };
            await _Context.Transactions.AddAsync(NewTransaction);

            var NewEntry = new TransactionEntry()
            {
                EntryType = EntryType.Out,
                TransactionId = NewTransaction.Id,
                AccountId = Account.Id
            };
            await _Context.TransactionEntries.AddAsync(NewEntry);

            return true;
        }

        
    }

}
