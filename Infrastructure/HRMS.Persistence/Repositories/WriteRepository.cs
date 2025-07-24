using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Repositories;
using HRMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly HRMSDbContext _context;

        public WriteRepository(HRMSDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove(int id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            Table.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveList(List<T> entity)
        {
            Table.RemoveRange(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            Table.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(List<T> entity)
        {
            Table.UpdateRange(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
