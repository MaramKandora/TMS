using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Transactions;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Interfaces.Transactions
{
    public interface ITransactionRepository
    {
        public Task<int> AddAsync(Transaction Transaction);

        public Task<Transaction?> GetByIdAsync(int Id);

        public Task<IEnumerable<Transaction>> GetAllAsync();

        public Task<bool> DepositAsync(string AccountNumber, decimal Amount);

        public Task<bool> WithdrawAsync(string AccountNumber, decimal Amount);

        public Task<bool> TransferAsync(string FromAccountNumber, string ToAccountNumber, decimal Amount);

    }
}
