using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Infrastructure.Database;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un pago por validación.
    /// </summary>
    public class AgregarPagoPorVerificacionCommandHandler : IRequestHandler<AgregarPagoPorverificacionCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarPagoPorVerificacionCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarPagoPorVerificacionCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarPagoPorVerificacionCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarPagoPorVerificacionCommandHandler> logger)
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
        public async Task<Guid> Handle(AgregarPagoPorverificacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("AgregarPagoVerificacionCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                return await HandleAsync(request);
            }
            catch (CustomException)
            {
                throw;
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
        private async Task<Guid> HandleAsync(AgregarPagoPorverificacionCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarPagoPorVerificacionCommandHandler.HandleAsync {Request}" , request);
                var deuda = _dbContext.Deuda.Include(o=>o.servicio).FirstOrDefault(x=>x.Id==request._idDeuda);
                var entity = PagoMapper.MapRequestPorValidacionEntity(deuda,request._idConsumidor,_dbContext);
                validarPago(entity);
                validarCamposEnlista(request._request.camposPagos);
                _dbContext.Pago.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarPagoPorVerificacionCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarPagoPorValidacionCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException("Error al realizar pago por validacion", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPagoPorValidacionCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw new CustomException(ex.Message);
            }  
        }
        public void validarPago(PagoEntity pago)
        {
            PagoDirectoValidator pagoDirectoValidator = new PagoDirectoValidator();
            ValidationResult resultado = pagoDirectoValidator.Validate(pago);
            if (!resultado.IsValid)
                throw new ValidationException(resultado.Errors);
        }
        
        public void validarCamposEnlista(List<CamposPagosRequest> listaCampos)
        {
            List<ValidationFailure> listaErrores=new List<ValidationFailure>();
            foreach (var campo in listaCampos)
            {
                CampoEnPagoValidator validator= new CampoEnPagoValidator();
                ValidationResult result2 = validator.Validate(campo);
                if (!result2.IsValid)
                    listaErrores.AddRange(result2.Errors);
            }
            if (listaErrores.Count>0)
                throw new ValidationException(listaErrores);
        }
    }
}
