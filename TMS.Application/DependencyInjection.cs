using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.People;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Interfaces.Transactions;
using TMS.Application.Services.People;
using TMS.Application.Services.TransactionEntries;
using TMS.Application.Services.Transactions;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionEntryService, TransactionEntryService>();
            return services;
        }
    }
}
