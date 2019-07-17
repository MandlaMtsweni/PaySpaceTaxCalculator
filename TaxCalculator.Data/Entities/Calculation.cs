using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Data.Entities
{
    public class Calculation
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public double TotalOwedIncomeTax { get; set; }
        public double Salary { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string InsertedUserId { get; set; }
        public int UpdatedUserId { get; set; }
    }
}
