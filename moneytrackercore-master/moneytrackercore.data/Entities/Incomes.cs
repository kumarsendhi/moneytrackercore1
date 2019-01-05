using System;
using System.Collections.Generic;
using System.Text;

namespace moneytrackercore.data.Entities
{
    public class Incomes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IncomeType Incometype { get; }
        public DateTime Date { get; set; }
        public decimal AmountEarned { get; set; }
        public string Comment { get; set; }

        public ICollection<Expenditure> Expenditure { get; set; }

    }
}
