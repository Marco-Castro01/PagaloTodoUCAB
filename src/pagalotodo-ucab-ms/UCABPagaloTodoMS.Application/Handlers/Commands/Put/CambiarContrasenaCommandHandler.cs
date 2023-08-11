using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para cambiar la contraseña.
    /// </summary>
    public class CambiarContrasenaCommandHandler : IRequestHandler<CambiarContrasenaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<CambiarContrasenaCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase CambiarContrasenaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Logger</param>
        public CambiarContrasenaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<CambiarContrasenaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(CambiarContrasenaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("CambiarContrasenaCommand.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return await HandleAsync(request);
            }
            catch (CustomException)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Guid> HandleAsync(CambiarContrasenaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("CambiarContrasenaCommand.HandleAsync {Request}", request);

                // Buscar el usuario que tiene el token de reinicio de contraseña
                var user = _dbContext.Usuarios.FirstOrDefault(u => u.PasswordResetToken == request._request.token && u.deleted==false);

                // Verificar si el usuario no existe o el token ha expirado
                if (user == null || user.ResetTokenExpires < DateTime.Now)
                {
                    _logger.LogWarning("Ha ocurrido un error: Token inválido");
                    throw new ArgumentNullException(nameof(user));
                }

                // Generar una nueva contraseña
                using (var hash = new HMACSHA512())
                {
                    user.passwordSalt = hash.Key;
                    user.passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(request._request.password));
                }

                user.ResetTokenExpires = null;
                user.PasswordResetToken = null;

                _dbContext.Usuarios.Update(user);
                await _dbContext.SaveEfContextChanges("APP");

                var id = user.Id;
                transaccion.Commit();
                _logger.LogInformation("CambiarContrasenaCommand.HandleAsync {Response}", id);
                return id;

            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Error CambiarContrasenaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al cambiar la contrasennia",ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CambiarContrasenaCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
