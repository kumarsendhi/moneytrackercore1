using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class BalanceModel
    {
        public int UserId { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}
