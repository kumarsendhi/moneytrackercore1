using System;
using System.Collections.Generic;
using System.Text;

namespace moneytrackercore.data.Entities
{
    public class Expenditure
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ExpenditureConfig ExpenditureCategory { get; }
        public PaymentMode PaymentMode { get; }
        public decimal AmountSpent { get; set; }
        public string Comment { get; set; }

        //public ICollection<Incomes> Incomes { get; set; }
    }
}
