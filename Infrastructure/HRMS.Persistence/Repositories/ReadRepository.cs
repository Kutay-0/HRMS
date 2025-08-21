using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Repositories;
using HRMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly HRMSDbContext _context;

        public ReadRepository(HRMSDbContext context)
        {
            _context = context;
        }

        //Set<T>() Bize T türünden entity değer döndürmemizi sağlayan metot
        public DbSet<T> Table => _context.Set<T>();

        //Veri tabanından sadece gerekli veriyi alır.
        //IEnumerable kullansaydık tüm veriyi çekip filtreleme yapacaktı.
        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await Table.FirstOrDefaultAsync(x => EF.Property<string>(x, "Id") == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            //işlemin bitmesini bekler ama uygulama çalışmaya devam eder.
            //await sadece async tanımlanmış metotlarda çalışabilir
            return await Table.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method);
        }
    }
}
