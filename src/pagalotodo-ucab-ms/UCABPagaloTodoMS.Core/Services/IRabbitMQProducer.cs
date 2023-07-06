namespace UCABPagaloTodoMS.Core.Services
{
    public interface IRabbitMQProducer
    {
        void PublishMessageToConciliacion_Queue(string message);
        
        void PublishMessageToVerificacion_Queue(string? message);
    }
}