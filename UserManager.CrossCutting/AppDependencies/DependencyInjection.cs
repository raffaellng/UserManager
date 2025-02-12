using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManager.Domain.Interfaces;
using UserManager.Infrastructure.Context;
using UserManager.Infrastructure.Repositories;

namespace UserManager.CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                                        this IServiceCollection services, 
                                        IConfiguration configuration)
        {
            var mySqlConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options => 
                                                options.UseMySql(mySqlConnection, 
                                                ServerVersion.AutoDetect(mySqlConnection)));

            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
