using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Infrastructure.Services
{
    public class RabbitMqConsumerConciliacionHS : IHostedService
    {
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqConsumerConciliacionHS(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _queueName = "conciliacion";

            _channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                await UpdateStatusPago(message);
                Console.WriteLine("Received message: " + message);

                _channel.BasicAck(args.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer
            );

            // Ejecuta el consumidor en segundo plano
            Task.Run(() => ConsumeMessagesAsync(_cancellationTokenSource.Token));

            return Task.CompletedTask;
        }

        public async Task UpdateStatusPago(string info)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IUCABPagaloTodoDbContext>();

                using (var transaccion = dbContext.BeginTransaction())
                {
                    try
                    {
                        var datos = info.Split(";");
                        var entityId = new Guid(datos[0]);
                        var entity = await dbContext.Pago.FindAsync(entityId);
                        
                        if (datos[1].Equals("aceptado"))
                        {
                            if (entity != null) entity.status = StatusPago.aceptado;
                        }
                        else if (datos[1].Equals("rechazado"))
                        {
                            if (entity != null) entity.status = StatusPago.rechazado;
                        }

                        dbContext.Pago.Update(entity);
                        dbContext.DbContext.SaveChanges();
                        await dbContext.SaveEfContextChanges("APP");
                        transaccion.Commit();
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                    }
                }
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            _channel.Close();
            _connection.Close();

            return Task.CompletedTask;
        }

        private async Task ConsumeMessagesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Ejecuta el consumo de mensajes en un bucle mientras no se haya cancelado
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
