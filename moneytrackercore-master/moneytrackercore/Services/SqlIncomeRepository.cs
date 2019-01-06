using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlIncomeRepository : IIncomeRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlIncomeRepository(moneytrackercoreDbContext context)
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

        public IEnumerable<Incomes> GetAllIncomes()
        {
            return _context.Incomes.OrderBy(i => i.Date).ToList();
        }
        
        public Incomes GetIncomes(int id)
        {
            return _context.Incomes.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
