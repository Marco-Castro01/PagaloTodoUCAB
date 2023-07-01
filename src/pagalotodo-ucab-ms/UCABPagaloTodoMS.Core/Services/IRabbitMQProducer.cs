namespace UCABPagaloTodoMS.Core.Services
{
    public interface IRabbitMQProducer
    {
        void PublishMessageToQueue1(string message);
        
        void PublishMessageToQueue2(string message);
    }
}