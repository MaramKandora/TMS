using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Transactions;

namespace TMS.Application.Interfaces.Transactions
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionDTO>> GetAllAsync();
        public Task<TransactionDTO?> GetByIDAsync(int Id);

        public Task<bool> WithdrawAsync(string AccountNumber, decimal Amount);

        public Task<bool> DepositAsync(string AccountNumber, decimal Amount);

        public Task<bool> TransferAsync(string FromAccountNumber, int ToAccountNumber, decimal Amount);
    }
}
