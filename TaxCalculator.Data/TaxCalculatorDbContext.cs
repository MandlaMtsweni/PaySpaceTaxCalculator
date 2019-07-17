using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data
{
    public class TaxCalculatorDbContext : IdentityDbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options)
          : base(options)
        {
        }

        public virtual DbSet<Calculation> Calculations { get; set; }
        public virtual DbSet<TaxBracket> TaxBrackets { get; set; }
    }
}
