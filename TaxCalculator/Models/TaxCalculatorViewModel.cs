using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Models
{
    public class TaxCalculatorViewModel
    {
        public Calculation Calculation { get; set; }
        public List<Calculation> calculations { get; set; }
        public TaxBracket TaxBracket { get; set; }
    }
}
