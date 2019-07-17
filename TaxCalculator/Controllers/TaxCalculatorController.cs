using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Data;
using TaxCalculator.Business.Features.Interfaces;
using TaxCalculator.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TaxCalculator.Controllers
{
   [Authorize]
    public class TaxCalculatorController : Controller
    {
        private readonly ITaxRepository _taxRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TaxCalculatorController(ITaxRepository taxRepository, UserManager<IdentityUser> userManager)
        {
            _taxRepository = taxRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string AnnualSalary, string postalCode, TaxCalculatorViewModel m)
        {
            var model = new TaxCalculatorViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.calculations = await _taxRepository.GetCalculations(user.Id);

            if(m.Calculation != null)
            {
                var amoutOwed = _taxRepository.TaxCalculation(Convert.ToDouble(m.Calculation.Salary), m.Calculation.PostalCode);

                var calc = new Calculation();

                calc.PostalCode = m.Calculation.PostalCode;
                calc.Salary = m.Calculation.Salary;
                calc.TotalOwedIncomeTax = amoutOwed;
                calc.InsertedUserId = user.Id;
                

                await _taxRepository.AddCalculation(calc);

                model.calculations.Add(calc);
            }
            
            return View(model);
        }
     
    }
}