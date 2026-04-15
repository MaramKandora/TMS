using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Entities.Transactions;

namespace TMS.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Domain.Entities.Transactions.Transaction> Transactions { get; set; }
        public DbSet<TransactionEntry> TransactionEntries { get; set; }
    }
}
