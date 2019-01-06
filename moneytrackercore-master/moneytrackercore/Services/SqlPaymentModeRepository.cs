using moneytrackercore.data.Entities;
using moneytrackercore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Services
{
    public class SqlPaymentModeRepository : IPaymentModeRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlPaymentModeRepository(moneytrackercoreDbContext context)
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

        public IEnumerable<PaymentMode> GetAllPaymentMode()
        {
            return _context.PaymentMode.OrderBy(i => i.PaymentConfig).ToList();
        }

        public PaymentMode GetPaymentMode(int id)
        {
            return _context.PaymentMode.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


    }
}
