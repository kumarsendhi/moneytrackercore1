using System;
using System.Collections.Generic;
using System.Text;

namespace moneytrackercore.data.Entities
{
    public class Balance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal BalanceAmount { get; set; }

        public ICollection<Incomes> Incomes { get; set; }
        public ICollection<Expenditure> Expenditure { get; set; }
    }
}
