using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.Accounts;

namespace TMS.Application.DTOs.Transactions
{
    public class TransferDTO
    {
        public required string FromAccountNumber { get; set; }
        public required string ToAccountNumber { get; set; }

        public decimal Amount { get; set; } 
    }
}
