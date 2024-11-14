using Core.MessageBroker.Interfaces;
using Core.MessageBroker.Services;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.MessageBroker
{
    public static class MessageBusDependencies
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, MessageBusOptions options)
        {

            services.AddHttpContextAccessor();

            services.AddMassTransit(x =>
            {
                options.Consumers(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(options.Host, h =>
                    {
                        h.Username(options.UserName);
                        h.Password(options.Password);
                    });

                    options.Endpoints(context, cfg);

                    // Register CorrelationId Observers for outgoing messages
                    var httpContextAccessor = context.GetRequiredService<IHttpContextAccessor>();
                    cfg.ConnectPublishObserver(new CorrelationIdPublishObserver(httpContextAccessor));
                    cfg.ConnectSendObserver(new CorrelationIdSendObserver(httpContextAccessor));
                });
            });

            if (options.IsProducer)
                services.AddSingleton<IMassTransitHandler, MassTransitHandler>();

            if (options.IntegrationEventBuilderType != null)
                services.AddSingleton(typeof(IIntegrationEventBuilder), options.IntegrationEventBuilderType);

            return services;
        }
    }
}
