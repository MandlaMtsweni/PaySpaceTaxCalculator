using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Business.Generics.Interfaces;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Business.Features.Interfaces
{
    public interface ITaxRepository : IGenericRepository<Calculation>
    {
        Task<List<TaxBracket>> GeTaxBrackets();
        Task<List<Calculation>> GetCalculations(string userId);
        double TaxCalculation(double annualIncome, string postalCode);
        double TaxCalculationOnflatRate(double annualIncome);
        double TaxCalculationOnFlateValue(double annualIncome);
        double TaxCalculationOnProgressiveTax(double annualIncome);

        Task AddCalculation(Calculation calculation);
        

    }
}
