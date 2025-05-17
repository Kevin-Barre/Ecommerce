using System.Text;
using System.Text.Json;
using app.FacturaSubscribe.Entities.Models;
using app.FacturaSubscribe.services.Config;
using app.FacturaSubscribe.services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace app.FacturaSubscribe.services.MQ
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly string NombreCola = "ventaDetallesQueue";



        public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger,
              IOptions<RabbitMQSettings> rabbitMQSettings, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQSettings = rabbitMQSettings.Value;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMQSettings.Hostname!,
                    Port = _rabbitMQSettings.Port!,
                    UserName = _rabbitMQSettings.Username!,
                    Password = _rabbitMQSettings.Password!,
                    VirtualHost = _rabbitMQSettings.VirtualHost!
                };

                var connection = await factory.CreateConnectionAsync();
                var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                       queue: NombreCola,
                       durable: true,
                       exclusive: false,
                       autoDelete: false
                   );

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    //var cuentas = JsonSerializer.Deserialize<List<PagoDto>>(message);
                    var ventaDetalle = JsonSerializer.Deserialize<VentaDetalle>(message);

                    _logger.LogInformation("Mensaje recibido: {Message}", message + "obj:" + ventaDetalle.NumeroItem);

                    // Crear un scope para obtener el servicio
                    using var scope = _serviceProvider.CreateScope();
                    var servicio = scope.ServiceProvider.GetRequiredService<IProcesoService>();

                    await servicio.GuardarVentaDetalleAsync(ventaDetalle);

                };

                await channel.BasicConsumeAsync(
                          queue: NombreCola,
                          autoAck: true,
                          consumer: consumer
                      );
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogError($"Error conectando con RabbitMQ: {ex.Message}");
            }
        }
    }
}
