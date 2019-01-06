using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class ExpenditureModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ExpenditureConfig ExpenditureCategory { get; }
        public PaymentMode PaymentMode { get; }
        public decimal AmountSpent { get; set; }
        public string Comment { get; set; }
    }
}
