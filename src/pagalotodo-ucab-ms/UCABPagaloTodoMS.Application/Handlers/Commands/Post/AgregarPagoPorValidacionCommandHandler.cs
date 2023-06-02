using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Infrastructure.Database;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un pago por validación.
    /// </summary>
    public class AgregarPagoPorValidacionCommandHandler : IRequestHandler<AgregarPagoPorValidacionCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarPagoPorValidacionCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarPagoPorValidacionCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarPagoPorValidacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarPagoPorValidacionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para agregar un pago por validación.
        /// </summary>
        /// <param name="request">Comando para agregar un pago por validación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del pago por validación agregado</returns>
        public async Task<Guid> Handle(AgregarPagoPorValidacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Request == null)
                {
                    _logger.LogWarning("AgregarPagoDirectoCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Maneja asincrónicamente el comando para agregar un pago por validación.
        /// </summary>
        /// <param name="request">Comando para agregar un pago por validación</param>
        /// <returns>Identificador del pago por validación agregado</returns>
        private async Task<Guid> HandleAsync(AgregarPagoPorValidacionCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarPagoPorValidacionCommandHandler.HandleAsync {Request}" , request);
                var entity = PagoMapper.MapRequestPorValidacionEntity(request.Request,_dbContext);
                PagoPorValidacionValidator pagoPorValidacionValidator = new PagoPorValidacionValidator();
                ValidationResult result = await pagoPorValidacionValidator.ValidateAsync(entity);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
                
                _dbContext.Pago.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarPagoPorValidacionCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPagoPorValidacionCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }
        }
    }
}
