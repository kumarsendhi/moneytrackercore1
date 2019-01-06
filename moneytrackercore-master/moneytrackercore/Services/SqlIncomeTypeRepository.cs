using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlIncomeTypeRepository : IIncomeTypeRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlIncomeTypeRepository(moneytrackercoreDbContext context)
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

        public IEnumerable<IncomeType> GetAllIncomeType()
        {
            return _context.IncomeType.OrderBy(i => i.IncomeTypeConfig).ToList();
        }

        public IncomeType GetIncomeType(int id)
        {
            return _context.IncomeType.FirstOrDefault(i => i.Id==id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
