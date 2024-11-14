using Core.Infrastructure;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("OracleConnection");


            //// Add services to the container
            services.AddDbContext<IdentityDbContext>((sp, options) =>
            {
                options.UseOracle(connectionString);
            });

            services.AddCoreInfrastructure(opt =>
            {

                // Http Client
                opt.EnableHttpClient = true;

                // Authentication
                //opt.EnableAuthentication = true;
                //opt.TokenOptions = configuration.GetSection("TokenOptions");

            });



            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IEmailServiceConfigRepository, EmailServiceConfigRepository>();
            services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();



            return services;
        }
    }
}
