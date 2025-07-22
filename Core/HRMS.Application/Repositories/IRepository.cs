using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Application.Repositories
{
    //Veri tabanı işlemlerini tekrar tekrar yazmak yerine bu işlemleri daha pratik şekilde tek seferde yapmak
    //T = Hangi entity ile çalışacaksak o
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table {  get; }
    }
}
