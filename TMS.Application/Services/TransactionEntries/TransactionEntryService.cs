using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Services.TransactionEntries
{
    public class TransactionEntryService : ITransactionEntryService
    {
        private readonly ITransactionEntryRepository _repo;
        public TransactionEntryService(ITransactionEntryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<TransactionEntryDTO>> GetAllAsync()
        {

           var Entries = await _repo.GetAllAsync();
            List<TransactionEntryDTO> DTOList = new List<TransactionEntryDTO>();

            foreach (var Entry in Entries)
            {
                DTOList.Add(_MapToDTO(Entry));
            }
            return DTOList;
        }

        public async Task<IEnumerable<TransactionEntryDTO>> GetAllFilteredAsync(TransactionEntriesFilterDTO dto)
        {
            var Entries = await _repo.GetAllFilteredAsync(dto);
            List<TransactionEntryDTO> DTOList = new List<TransactionEntryDTO>();

            foreach (var Entry in Entries)
            {
                DTOList.Add(_MapToDTO(Entry));
            }
            return DTOList;
        }

        public async Task<IEnumerable<TransactionEntryDTO>> GetAllByAccountIdAsync(int AccountId)
        {
            var Entries = await _repo.GetAllByAccountIdAsync(AccountId);
            List<TransactionEntryDTO> DTOList = new List<TransactionEntryDTO>();

            foreach (var Entry in Entries)
            {
                DTOList.Add(_MapToDTO(Entry));
            }
            return DTOList;
        }

        public async Task<TransactionEntryDTO?> GetByIdAsync(int Id)
        {
            var Entry = await _repo.GetByIdAsync(Id);

            return Entry is null
                ? null
                :_MapToDTO(Entry);
        }

        private TransactionEntryDTO _MapToDTO(TransactionEntry Entry)
        {
            return new TransactionEntryDTO()
            {
                Id = Entry.Id,
                AccountID = Entry.AccountId,
                TransactionID = Entry.TransactionId,
                EntryType = Entry.EntryType
            };

        }
    }
}
