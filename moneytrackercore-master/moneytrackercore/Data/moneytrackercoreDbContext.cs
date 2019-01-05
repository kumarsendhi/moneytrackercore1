using Microsoft.EntityFrameworkCore;
using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Data
{
    public class moneytrackercoreDbContext : DbContext
    {
        public moneytrackercoreDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<Incomes> Incomes { get; set; }
        public DbSet<Expenditure> Expenditure { get; set; }
        public DbSet<ExpenditureConfig> ExpenditureConfig { get; set; }
        public DbSet<PaymentMode> PaymentMode { get; set; }
        public DbSet<IncomeType> IncomeType { get; set; }
    }
}
