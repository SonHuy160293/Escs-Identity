using Core.Application.Services;
using Core.Infrastructure.Logging;
using Core.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Core.Infrastructure.Dependencies
{
    public static class HttpClientDependencies
    {
        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
        {
            //services.AddHttpClient("client")
            //    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500))) // retry 3 times with a delay of 500 milliseconds between each retry when exception occured 
            //    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10)));

            services.AddTransient<LoggingDelegatingHandler>();

            services.AddHttpClient("client")
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddScoped<IHttpCaller, HttpCaller>();

            return services;
        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        // Log the retry attempt
                        Console.WriteLine($"Retry {retryCount} for {context.PolicyKey} at {context.OperationKey}. Exception: {exception.ToString()}");
                    }
                );
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }

    }
}
