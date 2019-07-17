using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data;
using TaxCalculator.Business.Features.Interfaces;
using TaxCalculator.Business.Generics.Repositories;



namespace TaxCalculator.Business.Features.Repositories
{
    public class TaxRepository : GenericRepository<Calculation>, ITaxRepository
    {
        private readonly TaxCalculatorDbContext _dbContext;
        private const double flatValue = 10000; // per year else 5% for individual earning less 200000 per year
        private const double flatRate = 0.175;

        public TaxRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCalculation(Calculation calculation)
        {
             await _dbContext.Calculations.AddAsync(calculation);
             await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TaxBracket>> GeTaxBrackets()
        {
            return await _dbContext.TaxBrackets.ToListAsync();
        }

        public async Task<List<Calculation>> GetCalculations(string userId)
        {
            return await _dbContext.Calculations.Where(x => x.InsertedUserId == userId).OrderByDescending(y => y.InsertedDate).ToListAsync();
        }

        public double TaxCalculation(double annualIncome, string postalCode)
        {
            double amountTaxed = 0;

            switch (postalCode)
            {
                case "7441":
                case "1000":
                    amountTaxed = TaxCalculationOnProgressiveTax(annualIncome);
                    break;
                case "A100":
                    amountTaxed = TaxCalculationOnFlateValue(annualIncome);
                    break;
                case "7000":
                    amountTaxed = TaxCalculationOnflatRate(annualIncome);
                    break;
                default:
                    ;
                    break;
            }

            return  amountTaxed;
        }
        public double TaxCalculationOnflatRate(double annualIncome)
        {
            double result =  (annualIncome * flatRate);

            return result;
        }
        public double TaxCalculationOnFlateValue(double annualIncome)
        {
            var result = 0.0;
            if (annualIncome < 200000)
            {
                result = (annualIncome * 0.05);

            }
            else
            {
                result = flatValue;
            }            

            return result;
        }
        public double TaxCalculationOnProgressiveTax(double amount)
        {
        
            try
            {
                if (amount > 372950)
                    return 0.35 * (amount - 201400) + 66462 /*TaxFor(372950)*/;
                else if (amount > 171550)
                    return 0.33 * (amount - 89300) + 25004 /*TaxFor(171550)*/;
                else if (amount > 82250)
                    return 0.28 * (amount - 48300) + 12075 /*TaxFor(82250)*/;
                else if (amount > 33950)
                    return 0.25 * (amount - 25600) + 3840 /*TaxFor(33950)*/;
                else if (amount > 8350)
                    return 0.15 * (amount - 8350) + 835 /*TaxFor(8350)*/;
                else
                    return 0.10 * amount;
            }
            catch(Exception ex)
            {
                throw ex;
            }
      
        }
    }
}
