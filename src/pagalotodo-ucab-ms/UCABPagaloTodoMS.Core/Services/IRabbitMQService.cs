using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Services
{
   
    public interface IRabbitMQService
    {
        IModel CreateModel();

        void PublishToQueue1(byte[] messageBody);

        void PublishToQueue2(byte[] messageBody);
        void Dispose();

    }
}