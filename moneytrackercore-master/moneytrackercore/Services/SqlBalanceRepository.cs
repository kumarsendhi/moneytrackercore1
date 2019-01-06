using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlBalanceRepository : IBalanceRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlBalanceRepository(moneytrackercoreDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public IEnumerable<Balance> GetAllBalance()
        {
            return _context.Balance.OrderBy(i => i.BalanceAmount).ToList();
        }

        public Balance GetBalance(int id)
        {
            return _context.Balance.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
