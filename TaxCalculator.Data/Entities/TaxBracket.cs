using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Data.Entities
{
    public class TaxBracket
    {
        public int Id { get; set; }
        public decimal SumRate { get; set; }
        public decimal RatePercentage { get; set; }
        public decimal Taxes { get; set; }
        public DateTime InsertedDate { get; set; }
        public int InsertedUserId { get; set; }
        public DateTime updatedDate { get; set; }
        public int UpdatedUserId { get; set; }
        //public Employee Employee { get; set; }

        //public User User { get; set; }
    }
}
