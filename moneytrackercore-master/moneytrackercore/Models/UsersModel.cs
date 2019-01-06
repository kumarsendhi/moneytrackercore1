using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}
