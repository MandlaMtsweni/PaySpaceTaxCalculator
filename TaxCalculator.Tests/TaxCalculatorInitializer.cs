using System;
using TaxCalculator.Data;
using System.Linq;
using TaxCalculator.Data.Entities;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Tests
{
    public class TaxCalculatorInitializer
    {
        public static void Initialize(TaxCalculatorDbContext context)
        {
            // Seed database with sample Taxes if there aren't any
            if (!context.TaxBrackets.Any())
                SeedProgressiveTax(context);
        }

        private static void SeedProgressiveTax(TaxCalculatorDbContext context)
        {
            try
            {
                var taxBrackets = new[]
                {
                    new TaxBracket
                    {
                        // 0 - 8350 
                        RatePercentage = 10,  SumRate = 8350, Taxes = 835, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    },
                    new TaxBracket
                    {
                        // 8351 - 33950
                        RatePercentage = 15,  SumRate = 25600, Taxes = 3840, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    },
                     new TaxBracket
                    {
                         // 82250-33951
                        RatePercentage = 25,  SumRate = 48300, Taxes = 12075, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    },
                    new TaxBracket
                    {
                        // 82251 - 171550
                        RatePercentage = 28,  SumRate = 89300, Taxes = 25004, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    },
                       new TaxBracket
                    {
                        // 171551 - 372950
                        RatePercentage = 33,  SumRate = 201400, Taxes = 66462, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    },
                       new TaxBracket
                    {
                        // 372951+
                        RatePercentage = 35,  SumRate = 372951, Taxes = 130533, InsertedDate = DateTime.Now,
                        InsertedUserId = 1, updatedDate = DateTime.Now, UpdatedUserId = 1
                    }
            };

                context.TaxBrackets.AddRange(taxBrackets);
                context.SaveChanges();
            }
            catch (Exception)
            {
                // Skip this insert
                //}
            }

        }
    }
}
