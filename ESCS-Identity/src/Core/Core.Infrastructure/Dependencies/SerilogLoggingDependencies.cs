using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Extensions;

namespace Core.Infrastructure.Dependencies
{
    public static class SerilogLoggingDependencies
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuation) =>
        {
            var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

            configuation
                 .Enrich.FromLogContext()
                 .Enrich.WithExceptionDetails()
                 .Enrich.WithMachineName()
                 .Enrich.WithRequestBody()
                 .Enrich.WithClientIp()
                 .Enrich.WithRequestQuery()
                 .Enrich.WithCorrelationIdHeader()
                 .WriteTo.Debug()
                 .WriteTo.Console()
                 .WriteTo.Elasticsearch(
                    new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticUri))
                    {
                        IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                 .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                 .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                 .ReadFrom.Configuration(context.Configuration);


        };
    }
}
