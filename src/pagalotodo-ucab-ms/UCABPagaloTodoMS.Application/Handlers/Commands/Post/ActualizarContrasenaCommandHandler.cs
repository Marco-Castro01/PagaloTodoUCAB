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
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para actualizar la contraseña de un usuario.
    /// </summary>
    public class ActualizarContrasenaCommandHandler : IRequestHandler<ActualizarContrasenaCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ActualizarContrasenaCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase ActualizarContrasenaCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public ActualizarContrasenaCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ActualizarContrasenaCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para actualizar la contraseña de un usuario.
        /// </summary>
        /// <param name="request">Comando para actualizar la contraseña de un usuario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del usuario con la contraseña actualizada</returns>
        public async Task<Guid> Handle(ActualizarContrasenaCommand request, CancellationToken cancellationToken)
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

        /// <summary>
        /// Maneja asincrónicamente el comando para actualizar la contraseña de un usuario.
        /// </summary>
        /// <param name="request">Comando para actualizar la contraseña de un usuario</param>
        /// <returns>Identificador del usuario con la contraseña actualizada</returns>
        private async Task<Guid> HandleAsync(ActualizarContrasenaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("ActualizarContrasenaCommand.HandleAsync {Request}", request);


                var user = _dbContext.Usuarios.FirstOrDefault(u => u.Id == request._id && u.deleted==false);

                if (user == null)
                {
                    throw new CustomException("No existe el usuario");
                }

                using (var hash = new HMACSHA512())
                {
                    user.passwordSalt = hash.Key;
                    user.passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(request._request.Password));
                }

                _dbContext.Usuarios.Update(user);
                _dbContext.DbContext.SaveChanges();
                var id = user.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("ActualizarContrasenaCommand.HandleAsync {Response}", id);
                return id;
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error ActualizarContrasenaCommand.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ActualizarContrasenaCommand.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
