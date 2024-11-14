using Core.Application;
using Identity.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddCoreApplication(Assembly.GetExecutingAssembly());
            TokenExtension.Initialize(configuration);

            return services;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder builder)
        {
            builder.UseCoreApplication();

            return builder;
        }
    }
}
