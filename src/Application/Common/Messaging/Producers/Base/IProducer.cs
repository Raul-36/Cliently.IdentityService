namespace Cliently.IdentityService.Infrastructure.Messaging.Services.Base
{
    public interface IProducer
    {
        Task PublishAsync(string queueName, object message);
    }
}