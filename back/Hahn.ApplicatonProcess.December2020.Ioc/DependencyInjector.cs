using Hahn.ApplicatonProcess.December2020.Data.Contexts;
using Hahn.ApplicatonProcess.December2020.Data.Repositories;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Ioc
{
    public class DependencyInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ApplicantService>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            services.Scan(scan => scan
                .FromAssemblyOf<ApplicantRepository>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            services.AddDbContext<ApplicantContext>(opt => opt.UseInMemoryDatabase("memoryDatabase"));
        }
    }
}