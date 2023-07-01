using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Infrastructure.Services
{
    public class RabbitMqConsumerVerificacionHS : IHostedService
    {
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqConsumerVerificacionHS(IServiceProvider serviceProvider)
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

            _queueName = "verificacion";

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
                await AddDoubt(message);
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

        public async Task AddDoubt(string datos)
        {
            string[] lines = datos.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<DeudaEntity> deudas = new List<DeudaEntity>();
            using (var scope=_serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IUCABPagaloTodoDbContext>();
                using (var transaccion=dbContext.BeginTransaction())
                {
                    try
                    {
                        ServicioEntity servicio=new ServicioEntity();
                        foreach (var line in lines)
                        {
                            if (!line.Contains("idServicio"))
                            {
                                var lineSplit = line.Split(';');
                                deudas.Add(new DeudaEntity()
                                    {
                                        Id =new Guid(),
                                        identificador =lineSplit[0],
                                        servicio = servicio,
                                        deuda = ConvertToDouble(lineSplit[1]),
                                        deudaStatus = false
                                    }
                                );
                                
                            }
                            else
                            {
                                servicio = await dbContext
                                    .Servicio
                                    .FindAsync(new Guid(line.Split(':')[1])) ?? throw new InvalidOperationException();
                            }
                        }
                        dbContext.Deuda.AddRange(deudas);
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
        public double ConvertToDouble(string value)
        {
            // Define las opciones de formato para permitir tanto el punto como la coma como separador decimal
            var numberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = ","
            };

            double result;

            // Intenta analizar el valor utilizando el formato especificado
            if (double.TryParse(value, NumberStyles.Float, numberFormat, out result))
            {
                return result;
            }

            // Si no se pudo analizar el valor, intenta nuevamente reemplazando el separador decimal
            value = value.Replace(",", ".");
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            // Si no se pudo convertir el valor, lanza una excepción o devuelve un valor predeterminado
            throw new ArgumentException("El valor no se pudo convertir a double.");
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
