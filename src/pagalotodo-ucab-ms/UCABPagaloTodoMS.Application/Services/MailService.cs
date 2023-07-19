using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace UCABPagaloTodoMS.Application.Services
{
    [ExcludeFromCodeCoverage]

    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;


        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task EnviarCorreoElectronicoAsync(string email, string codigo)
        {
            var smtpConfig = _configuration.GetSection("SmtpConfig");

            var smtpServer = smtpConfig["SmtpServer"];
            var smtpPort = int.Parse(smtpConfig["SmtpPort"]);
            var smtpUsername = smtpConfig["SmtpUsername"];
            var smtpPassword = smtpConfig["SmtpPassword"];

            var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            };

            var message = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "Código de recuperación de contraseña",
                Body = $"Su código de recuperación de contraseña es: {codigo}. Este código es válido por 5 minutos."
            };

            message.To.Add(email);
            await smtpClient.SendMailAsync(message);
        }


        public async Task EnviarCorreoElectronicoConciliacionAsync(string email, string urlConciliacion)
        {
            var smtpConfig = _configuration.GetSection("SmtpConfig");

            var smtpServer = smtpConfig["SmtpServer"];
            var smtpPort = int.Parse(smtpConfig["SmtpPort"]);
            var smtpUsername = smtpConfig["SmtpUsername"];
            var smtpPassword = smtpConfig["SmtpPassword"];

            var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            };

            var message = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "Cierre Contable de su servicio. PagalotodoUCAB",
                Body = $"Haga click en este link para descargar su archivo de conciliacion es:  {urlConciliacion}   Att:PagaloTodoUCAB."
            };

            message.To.Add(email);
            await smtpClient.SendMailAsync(message);
        }
    }
}