using Core.Application.Services;
using Core.Domain.Helpers;
using Core.Infrastructure.Dependencies;
using Core.Infrastructure.DependencyModels;
using Core.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, Action<DependencyOptions> options)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddHttpContextAccessor();
            CorrelationIdProvider.Configure(services.BuildServiceProvider().GetService<IHttpContextAccessor>());

            services.AddOptions<DependencyOptions>().Configure(options);

            var dependencyOptions = services.BuildServiceProvider().GetService<IOptions<DependencyOptions>>();

            // TODO: Bug
            if (dependencyOptions.Value.EnableDbContextHandler)
                services.AddScoped<IDbContextHandler, DbContextHandler>();


            if (dependencyOptions.Value.EnableHttpClient)
                services.AddCustomHttpClient();



            if (dependencyOptions.Value.EnableAuthentication)
            {
                services.Configure<TokenOptions>(dependencyOptions.Value.TokenOptions);
                services.AddCustomAuthentication();
            }

            return services;
        }

        //public static ILoggingBuilder AddCoreLogging(this ILoggingBuilder builder, IConfiguration configuration, Action<LoggingOptions> loggingOptions)
        //{
        //    var options = new LoggingOptions();
        //    loggingOptions(options);

        //    var loggerConfiguration = new LoggerConfiguration()
        //        .ReadFrom.Configuration(configuration)
        //        .Enrich.FromLogContext()
        //        .Enrich.WithCorrelationIdHeader("CorrelationId");

        //    if (options.EnableElasticLogging)
        //        loggerConfiguration.WriteTo.Elasticsearch(ConfigureElasticSink(options));

        //    var logger = loggerConfiguration.CreateLogger();

        //    builder.ClearProviders();
        //    builder.AddSerilog(logger);

        //    return builder;
        //}

        //private static ElasticsearchSinkOptions ConfigureElasticSink(LoggingOptions loggingOptions)
        //{
        //    return new ElasticsearchSinkOptions(new Uri(loggingOptions.ElasticUri))
        //    {
        //        AutoRegisterTemplate = true,
        //        IndexFormat = $"{loggingOptions.ApplicationName}-{DateTime.UtcNow:yyyy}"
        //    };
        //}
    }
}
