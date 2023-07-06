using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mailing;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ResetQueryHandler : IRequestHandler<PasswordResetQuery, PasswordResetResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ResetQueryHandler> _logger;
        private readonly IEmailSender _emailSender;

        public ResetQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ResetQueryHandler> logger, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _logger = logger;
            _emailSender = emailSender;
        }

        public Task<PasswordResetResponse> Handle(PasswordResetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("PasswordReset.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
            }
            catch (ArgumentNullException ex)
            {
                throw new CustomException("Request Nulo: ", ex);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw new CustomException("Error En consulta: ",ex);
            }
        }

        private async Task<PasswordResetResponse> HandleAsync(PasswordResetQuery usuario)
        {
            try
            {
                _logger.LogInformation("PasswordResetQuery.HandleAsync");

                // Buscar el usuario en la base de datos por su correo electrónico
                var usuariocreated = _dbContext.Usuarios.Where(c => c.email == usuario._request.email && c.deleted==false).FirstOrDefault();
                if (usuariocreated == null)
                {
                    _logger.LogWarning("Ha ocurrido un error: el Usuario no existe");
                    throw new ArgumentNullException(nameof(usuariocreated));
                }

                // Generar un token de restablecimiento de contraseña
                usuariocreated.PasswordResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

                // Enviar un correo electrónico al usuario con el token de restablecimiento de contraseña
                var message = new Message(new string[] {usuariocreated.email}, "Código de reseteo de clave",
                    usuariocreated.PasswordResetToken);
                _emailSender.SendEmail(message);

                // Establecer la fecha de vencimiento del token de restablecimiento de contraseña
                usuariocreated.ResetTokenExpires = DateTime.Now.AddDays(1);

                // Actualizar el usuario en la base de datos
                _dbContext.Usuarios.Update(usuariocreated);
                _dbContext.DbContext.SaveChanges();

                return new PasswordResetResponse
                {
                    email = usuario._request.email,
                    info = "Token enviado"
                };
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error .HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en Reset: ",ex);
            }
        }
    }
}
