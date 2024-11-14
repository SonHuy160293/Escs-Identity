using Core.MessageBroker.Interfaces;
using MassTransit;

namespace Core.MessageBroker.Services
{
    public class MassTransitHandler : IMassTransitHandler
    {
        private readonly IServiceProvider _serviceProvider;
        public MassTransitHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(object @event)
        {
            try
            {

                var bus = (IBus)this._serviceProvider.GetService(typeof(IBus));
                await bus.Publish(@event);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public async Task Send(string queueName, object @event)
        {
            // Ensure queueName is formatted as a URI with 'queue:' prefix
            var uri = new Uri($"queue:{queueName}");
            var bus = (IBus)this._serviceProvider.GetService(typeof(IBus));
            var endPoint = await bus.GetSendEndpoint(uri);

            await endPoint.Send(@event);
        }
    }
}
