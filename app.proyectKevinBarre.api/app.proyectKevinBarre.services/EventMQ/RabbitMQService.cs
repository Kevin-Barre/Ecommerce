using System.Text;
using app.projectKevinBarre.common.EventMQ;
using app.projectKevinBarre.services.eventMQ;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace app.projectKevinBarre.services.EventMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQSettings _settings;

        public RabbitMQService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public async Task PublishMessage<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Hostname!,
                Port = _settings.Port,
                UserName = _settings.Username!,
                Password = _settings.Password!,
                VirtualHost = _settings.VirtualHost!,
            };


            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
           
            await channel.QueueDeclareAsync(queue: queueName,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);

        }
    }
}
