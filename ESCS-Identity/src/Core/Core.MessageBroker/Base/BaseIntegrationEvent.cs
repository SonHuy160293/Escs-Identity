namespace Core.MessageBroker.Entities
{
    public interface BaseIntegrationEvent
    {
        public string CorrelationId { get; set; }
        public string Type { get; set; }
    }
}
