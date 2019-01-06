using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlExpenditureConfigRepository : IExpenditureConfigRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlExpenditureConfigRepository(moneytrackercoreDbContext context)
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

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

     

        IEnumerable<ExpenditureConfig> IExpenditureConfigRepository.GetAllExpenditureConfig()
        {
            return _context.ExpenditureConfig.OrderBy(i => i.ExpenditureCategory).ToList();
        }

        ExpenditureConfig IExpenditureConfigRepository.GetExpenditureConfig(int id)
        {
            return _context.ExpenditureConfig.FirstOrDefault(i => i.Id == id);
        }

    }
}
