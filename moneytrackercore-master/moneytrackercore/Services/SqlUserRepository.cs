using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using moneytrackercore.data.Entities;
using moneytrackercore.Data;

namespace moneytrackercore.Services
{
    public class SqlUserRepository : IUserRepository
    {
        private moneytrackercoreDbContext _context;

        public SqlUserRepository(moneytrackercoreDbContext context)
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

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.OrderBy(i => i.FirstName).ToList();
        }

        public Users GetUser(int id)
        {
            return _context.Users.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
