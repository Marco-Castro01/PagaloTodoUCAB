using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un consumidor.
    /// </summary>
    public class AgregarConsumidorCommandHandler : IRequestHandler<AgregarConsumidorCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarConsumidorCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarConsumidorCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarConsumidorCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarConsumidorCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para agregar un consumidor.
        /// </summary>
        /// <param name="request">Comando para agregar un consumidor</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del consumidor agregado</returns>
        public async Task<string> Handle(AgregarConsumidorCommand request, CancellationToken cancellationToken)
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
        /// Maneja asincrónicamente el comando para agregar un consumidor.
        /// </summary>
        /// <param name="request">Comando para agregar un consumidor</param>
        /// <returns>Identificador del consumidor agregado</returns>
        private async Task<string> HandleAsync(AgregarConsumidorCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarUsuarioCommand.HandleAsync {Request}", request);

                var entity = UsuariosMapper.MapRequestConsumidorEntity(request._request);
                List<UsuarioEntity> consumidores = await _dbContext
                    .Usuarios
                    .Where(x => x.email == entity.email || x.cedula == entity.cedula || x.nickName == entity.nickName).ToListAsync();

                if (consumidores != null && consumidores.Any())
                    throw new CustomException(400, datosyaExistentes(consumidores, entity));


                ConsumidorValidator usuarioValidator = new ConsumidorValidator();
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
                return "Registro exitoso";
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al agregar un consumidor", ex);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
            


            
        }


        public string datosyaExistentes(List<UsuarioEntity> consumidores, ConsumidorEntity consumidor)
        {
            string mensaje = "";

            if (consumidores.Any(x => x.email.ToLower() == consumidor.email.ToLower()))
                mensaje += "El Email '" + consumidor.email + "' ya está en uso.\n";
            if (consumidores.Any(x => x.cedula == consumidor.cedula))
                mensaje += "La cedula '" + consumidor.cedula + "' ya está en uso.\n";            
            if (consumidores.Any(x => x.nickName == consumidor.nickName))
                mensaje += "El nickName '" + consumidor.nickName + "' ya está en uso.\n";

            return mensaje;
        }

    }
}
