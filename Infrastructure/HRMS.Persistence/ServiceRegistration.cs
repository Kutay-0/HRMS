using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HRMS.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HRMS.Application.Repositories;
using HRMS.Persistence.Repositories;
using HRMS.Application.Abstractions.Token;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.Persistence
{
    //Extension metot örneği. Program.cs doyasısını daha düzenli, daha moduler tutmak.
    //Tüm Persistence Servisleri burada olabilir (Repositoryler)
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            //Hangi sunucuya işlem yapacağımızı bildirdik.
            services.AddDbContext<HRMSDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
        }
    }
}
