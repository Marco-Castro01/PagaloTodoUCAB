using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    /// Clase que maneja el comando para agregar un pago directo.
    /// </summary>
    public class AgregarPagoDirectoCommandHandler : IRequestHandler<AgregarPagoDirectoCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AgregarPagoDirectoCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarPagoDirectoCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public AgregarPagoDirectoCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AgregarPagoDirectoCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comando para agregar un pago directo.
        /// </summary>
        /// <param name="request">Comando para agregar un pago directo</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del pago directo agregado</returns>
        public async Task<Guid> Handle(AgregarPagoDirectoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.DirectoRequest == null)
                {
                    _logger.LogWarning("AgregarPagoDirectoCommandHandler.Handle: Request nulo.");
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
        /// Maneja asincrónicamente el comando para agregar un pago directo.
        /// </summary>
        /// <param name="request">Comando para agregar un pago directo</param>
        /// <returns>Identificador del pago directo agregado</returns>
        private async Task<Guid> HandleAsync(AgregarPagoDirectoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarPagoDirectoCommandHandler.HandleAsync {Request}" , request);
                
                var entity = PagoMapper.MapRequestDirectoEntity(request,_dbContext);
                validarPago(entity);
                validarCamposEnlista(request.DirectoRequest.camposPagos);
                entity.formatoPago = JsonConvert.SerializeObject(request.DirectoRequest.camposPagos);
                _dbContext.Pago.Add(entity);
                var id = entity.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarPagoDirectoCommandHandler.HandleAsync {Response}", id);
                return id;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarPagoDirectoCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw CustomException.CrearDesdeListaException(422,"Error al registrar pago: ", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPagoDirectoCommandHandlerAgregarPagoCommandHandler.HandleAsync. {Mensaje}", ex.Message);
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
