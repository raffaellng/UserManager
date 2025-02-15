using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;
using System.Reflection;
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
            var myhamdlers = AppDomain.CurrentDomain.Load("UserManager.Application");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhamdlers));

            services.AddDbContext<AppDbContext>(options =>
                                                options.UseMySql(mySqlConnection,
                                                ServerVersion.AutoDetect(mySqlConnection)));

            services.AddSingleton<IDbConnection>(provider =>
            {
                var connection = new MySqlConnection(mySqlConnection);
                connection.Open();
                return connection;
            });


            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();

            services.AddValidatorsFromAssembly(Assembly.Load("UserManager.Application"));

            return services;
        }
    }
}
