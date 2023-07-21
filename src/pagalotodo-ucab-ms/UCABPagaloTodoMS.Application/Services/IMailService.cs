namespace UCABPagaloTodoMS.Application.Services
{
    public interface IMailService
    {
        Task EnviarCorreoElectronicoAsync(string email, string bodyMessage);
        Task EnviarCorreoElectronicoConciliacionAsync(string email, string bodyMessage);
    }
}