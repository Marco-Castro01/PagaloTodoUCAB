using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un administrador.
    /// </summary>
    public class AgregarAdminCommandHandler : IRequestHandler<AgregarAdminCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarAdminCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarAdminCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarAdminCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarAdminCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para agregar un administrador.
        /// </summary>
        /// <param name="request">Comando para agregar un administrador</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del administrador agregado</returns>
        public async Task<Guid> Handle(AgregarAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return await HandleAsync(request);
            }
            catch (CustomException)
            {
                throw;

            } 
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para agregar un administrador.
        /// </summary>
        /// <param name="request">Comando para agregar un administrador</param>
        /// <returns>Identificador del administrador agregado</returns>
        private async Task<Guid> HandleAsync(AgregarAdminCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarUsuarioCommand.HandleAsync {Request}", request);
                var entity = UsuariosMapper.MapRequestAdminEntity(request._request);
                AdminValidator usuarioValidator = new AdminValidator();
                ValidationResult result = await usuarioValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                _dbContext.Usuarios.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit(); 
                _logger.LogInformation("AgregarAdminHandler.HandleAsync {Response}", id);
                return id;
            }catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al agregar un admin", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
