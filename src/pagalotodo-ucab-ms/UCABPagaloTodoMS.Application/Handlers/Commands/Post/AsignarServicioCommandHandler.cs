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
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un administrador.
    /// </summary>
    public class AsignarServicioCommandHandler : IRequestHandler<AsignarServicioComand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AsignarServicioCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarAdminCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AsignarServicioCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AsignarServicioCommandHandler> logger)
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
        public async Task<Guid> Handle(AsignarServicioComand request, CancellationToken cancellationToken)
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
            catch (ArgumentNullException ex)
            {
                throw new CustomException("Request Nulo", ex);
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
        private async Task<Guid> HandleAsync(AsignarServicioComand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarUsuarioCommand.HandleAsync {Request}", request);
                var entity = ServicioMapper.MapRequestEntity(request._prestadorServicioId,request._request,_dbContext);
                ServicioValidator servicioValidator = new ServicioValidator();
                ValidationResult result = await servicioValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }               
                _dbContext.Servicio.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AsignarServicioComandHandle.HandleAsync {Response}", id);
                return id;
            }catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw CustomException.CrearDesdeListaException(422, "Se produjeron los siguientes errores de validación",ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AsignarServicioComandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
