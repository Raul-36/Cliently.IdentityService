using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Cliently.IdentityService.Infrastructure.Messaging.Options;
using Cliently.IdentityService.Infrastructure.Messaging.Services.Base;
using System.Threading.Tasks;

namespace Cliently.IdentityService.Infrastructure.Messaging.Services;
public class RabbitMqProducer : IProducer, IDisposable
{
    private readonly ConnectionFactory factory;
    private IConnection? connection;
    private IChannel? channel;

    public RabbitMqProducer(IOptions<RabbitMQOptions> rabbitMqOptions)
    {
        factory = new ConnectionFactory
        {
            HostName = rabbitMqOptions.Value.HostName,
            UserName = rabbitMqOptions.Value.UserName,
            Password = rabbitMqOptions.Value.Password
        };
    }

    private async Task EnsureConnectedAsync()
    {
        if (connection != null && channel != null && channel.IsOpen) 
            return;

        connection = await factory.CreateConnectionAsync();
        channel = await connection.CreateChannelAsync();
    }

    public async Task PublishAsync(string queueName, object message)
    {
        await EnsureConnectedAsync();

        if (channel == null)
            throw new InvalidOperationException("RabbitMQ channel is not initialized.");

        await channel.QueueDeclareAsync(
            queue: queueName, 
            durable: true, 
            exclusive: false, 
            autoDelete: false
        );

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties { ContentType = "application/json" };

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            basicProperties: props,
            body: body
        );
    }

    public void Dispose()
    {
        channel?.Dispose();
        connection?.Dispose();
    }
}