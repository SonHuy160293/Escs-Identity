using Core.MessageBroker.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace Core.MessageBroker.Services
{
    internal class MasstransitObserverHandler
    {
    }

    public class CorrelationIdPublishObserver : IPublishObserver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdPublishObserver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            // Check if HttpContext is available
            var correlationId = _httpContextAccessor.HttpContext?.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                                ?? Guid.NewGuid().ToString();

            context.Headers.Set("CorrelationId", correlationId);
            Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId);

            return Task.CompletedTask;
        }

        public Task PostPublish<T>(PublishContext<T> context) where T : class => Task.CompletedTask;
        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class => Task.CompletedTask;
    }

    public class CorrelationIdSendObserver : ISendObserver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdSendObserver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task PreSend<T>(SendContext<T> context) where T : class
        {

            string correlationId = "";
            if (_httpContextAccessor.HttpContext is not null)
            {
                // Check if HttpContext is available
                correlationId = _httpContextAccessor.HttpContext?.Request.Headers["CorrelationId"].FirstOrDefault()
                                   ?? Guid.NewGuid().ToString();
            }

            if (context.Message is BaseIntegrationEvent baseEvent)
            {
                // Now you can access the fields of the EmailSentEvent message
                correlationId = baseEvent.CorrelationId;

            }

            context.Headers.Set("CorrelationId", correlationId);
            Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId);

            return Task.CompletedTask;
        }

        public Task PostSend<T>(SendContext<T> context) where T : class => Task.CompletedTask;
        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class => Task.CompletedTask;
    }


    public class CorrelationIdMiddleware<T> : IFilter<ConsumeContext<T>> where T : class
    {
        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            // Try to retrieve CorrelationId from headers
            if (context.Headers.TryGetHeader("X-Correlation-ID", out var correlationId))
            {
                Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId); // Push to LogContext for this message's lifecycle
            }
            //else
            //{
            //    var newCorrelationId = Guid.NewGuid().ToString();
            //    context.Headers. = newCorrelationId;
            //    Serilog.Context.LogContext.PushProperty("CorrelationId", newCorrelationId);  // Set new CorrelationId if missing
            //}

            return next.Send(context); // Pass to next pipe in pipeline
        }

        public void Probe(ProbeContext context) { context.CreateFilterScope("CorrelationIdMiddleware"); }
    }

}
