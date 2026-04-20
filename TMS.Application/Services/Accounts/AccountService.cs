using System;
using TMS.Application.DTOs.Accounts;
using TMS.Application.Interfaces.Accounts;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.People;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TMS.Application.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddAsync(AccountToAddDTO dto)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth
            };

            var account = new Account
            {
                Person = person,
                Number = DateTime.Now.Ticks.ToString()[^4..],
                Password = dto.Password,
                Balance = 0,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            return await _repo
                .AddAsync(account);
        }

        public async Task<bool> UpdateAsync(AccountToUpdateDTO dto)
        {
            var account = await _repo
                .GetByIdAsync(dto.Id);

            if (account is null || account.Person is null)  return false; 

            account.Person.FirstName = dto.FirstName;
            account.Person.LastName = dto.LastName;
            account.Person.Email = dto.Email;
            account.Person.Phone = dto.Phone;
            account.Person.DateOfBirth = dto.DateOfBirth;

            return await _repo
                .UpdateAsync(account);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _repo
                .GetByIdAsync(id);

            return account is not null && await _repo.DeleteAsync(account);
        }

        public async Task<bool> ActivateAsync(int id, bool activate)
        {
            var account = await _repo
                .GetByIdAsync(id);

            if (account is null) return false;

            return await _repo
                .ActivateAsync(account, activate);
        }

        public async Task<bool> ChangePasswordAsync(int id, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword) return false;

            var account = await _repo
                .GetByIdAsync(id);

            if (account is null) return false;

            return await _repo
                .ChangePasswordAsync(account, newPassword);
        }

        public async Task<AccountDTO?> GetByIdAsync(int id)
        {
            var account = await _repo
                .GetByIdAsync(id);

            return account is null 
                ? null 
                : MapToDTO(account);
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _repo
                .GetAllAsync();

            return accounts
                .Select(MapToDTO);
        }

        private static AccountDTO MapToDTO(Account account)
        {
            return new AccountDTO
            {
                Id = account.Id,
                Number = account.Number,
                Balance = account.Balance,
                IsActive = account.IsActive,
                // بيانات الشخص
                FirstName = account.Person?.FirstName ?? string.Empty,
                LastName = account.Person?.LastName ?? string.Empty,
                Email = account.Person?.Email ?? string.Empty,
                Phone = account.Person?.Phone,
                DateOfBirth = account.Person?.DateOfBirth ?? DateTime.Now
            };
        }

        public async Task<AccountDTO?> GetByNumberAsync(string number)
        {
            var account = await _repo
                .GetByNumberAsync(number);

            return account is null
                ? null
                : MapToDTO(account);
        }

        public async Task<bool> UpdateBalanceAsync(string number, decimal newBalance)
        {
            if (string.IsNullOrWhiteSpace(number) || newBalance < 0) return false;

            var account = await _repo
                .GetByNumberAsync(number);

            return account is not null && await _repo.UpdateBalanceAsync(number, newBalance);
        }

        public async Task<AccountDTO?> LoginAsync(AccountToLoginDTO dto)
        {
            if (dto is null
                || string.IsNullOrWhiteSpace(dto.Number) 
                || string.IsNullOrWhiteSpace(dto.Password))
            {
                return null;
            }

            var account = await _repo
                .GetByNumberAsync(dto.Number);

            if (account is null) return null;

            var result = account.Number == dto.Number 
                && account.Password == dto.Password;

            return result 
                ? MapToDTO(account)
                : null;
        }

    }
}