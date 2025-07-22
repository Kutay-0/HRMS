using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> RemoveAsync(List<T> entity);
        Task<bool> Remove(int id);
        Task<bool> UpdateAsync(int id);
        Task<bool> UpdateAsync(List<T> entity);
    }
}
