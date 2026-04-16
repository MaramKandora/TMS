using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Transactions;
using TMS.Domain.Entities.Transactions;

namespace TMS.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

       
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            var transactions = await _repo.GetAllAsync();

            foreach (var transaction in transactions)
            {
                DTOList.Add(MapToDTO(transaction));
            }

            return DTOList;
        }

        public async Task<TransactionDTO?> GetByIdAsync(int Id)
        {
            var transaction = await _repo.GetByIdAsync(Id);
            return transaction is null
                ? null : MapToDTO(transaction);
        }

        public Task<int?> TransferAsync(TransferDTO dto) 
        {
            return _repo.TransferAsync(dto);
        }

        public async Task<int?> DepositAsync(DepositDTO dto)
        {
            return await _repo.DepositAsync(dto);
        }

        public async Task<int?> WithdrawAsync(WithdrawDTO dto)
        {
            return await _repo.WithdrawAsync(dto);
        }

        private TransactionDTO MapToDTO(Transaction transaction)
        {
            return new TransactionDTO()
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Entries = transaction.Entries
            };
        }
    }
}
