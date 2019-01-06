using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlExpenditureRepository : IExpenditureRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlExpenditureRepository(moneytrackercoreDbContext context)
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

        public IEnumerable<Expenditure> GetAllExpenditure()
        {
            return _context.Expenditure.OrderBy(i => i.Date).ToList();
        }

        public Expenditure GetExpenditure(int id)
        {
            return _context.Expenditure.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
