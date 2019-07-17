using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Data;
using TaxCalculator.Business.Features.Repositories;


namespace TaxCalculator.Tests
{
    public class TestBase : IDisposable
    {
        public IConfiguration Configuration;
        protected readonly TaxCalculatorDbContext _dbContext;
        public TaxRepository taxRepository;
        public TestBase()
        {
            // Adding JSON file into IConfiguration.
            var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", true, true);
            Configuration = config.Build();

            // Integration Test Setup
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            // Configuration for TaxCalculatorDb
            var builder = new DbContextOptionsBuilder<TaxCalculatorDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("TaxCalculationConnetion"))                                                                    
                    .UseInternalServiceProvider(serviceProvider);
            _dbContext = new TaxCalculatorDbContext(builder.Options);
            _dbContext.Database.EnsureCreated();

            taxRepository = new TaxRepository(_dbContext);

            TaxCalculatorInitializer.Initialize(_dbContext);
           
        }

        public void Dispose()
        {
            // Integration : Sql Server           
            _dbContext.Dispose();
        }

    }
}
