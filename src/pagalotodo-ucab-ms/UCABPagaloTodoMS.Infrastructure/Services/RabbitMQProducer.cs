using System.Text;
using RabbitMQ.Client;
using UCABPagaloTodoMS.Core.Services;

namespace UCABPagaloTodoMS.Infrastructure.Services
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQProducer(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public void PublishMessageToQueue1(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _rabbitMQService.PublishToQueue1(body);
        }

        public void PublishMessageToQueue2(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _rabbitMQService.PublishToQueue2(body);
        }

        public void PublishMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}