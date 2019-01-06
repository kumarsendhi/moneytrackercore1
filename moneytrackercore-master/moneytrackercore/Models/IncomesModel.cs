using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class IncomesModel
    {
        public int UserId { get; set; }
        
        public DateTime Date { get; set; }
        public decimal AmountEarned { get; set; }
        public string Comment { get; set; }

        public string IncometypeIncomeTypeConfig { get; set; }
    }
}
