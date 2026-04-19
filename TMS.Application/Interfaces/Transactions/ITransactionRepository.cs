using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Interfaces.Transactions
{
    public interface ITransactionRepository
    {

        public Task<Transaction?> GetByIdAsync(int Id);

        public Task<IEnumerable<Transaction>> GetAllDepositsAsync(string? AccountNumber);

        public Task<IEnumerable<Transaction>> GetAllWithdrawsAsync(string? AccountNumber);
        public Task<IEnumerable<Transaction>> GetAllTransfersAsync(GetTransferDTO dto);
        public Task<int?> AddAsync(TransactionType Type, decimal Amount);
      

    }
}
