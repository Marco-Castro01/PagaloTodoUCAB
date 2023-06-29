using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class ConfFormatoPagoCommandHandler : IRequestHandler<ConfFormatoPagoCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConfFormatoPagoCommandHandler> _logger;

        /// <summary>
        /// Constructor de la clase AgregarAdminCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public ConfFormatoPagoCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConfFormatoPagoCommandHandler> logger)
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
        public async Task<Guid> Handle(ConfFormatoPagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
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
        private async Task<Guid> HandleAsync(ConfFormatoPagoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarUsuarioCommand.HandleAsync {Request}", request);

                var servicio = _dbContext.Servicio.Include(o => o.PrestadorServicio)
                    .FirstOrDefault(c => c.Id == request._ServicioId);
                if (servicio == null)
                    throw new NullReferenceException();
                validarCamposEnlista(request._ListaCamposPagos);
                List<CamposPagosRequest> listaCampos;
                if (servicio.formatoDePagos==null || servicio.formatoDePagos.Equals("")|| servicio.formatoDePagos.Equals(" "))
                {
                    listaCampos = new List<CamposPagosRequest>();

                }
                else
                {
                    listaCampos = JsonConvert.DeserializeObject<List<CamposPagosRequest>>(servicio.formatoDePagos);
                }
                
                listaCampos.AddRange(request._ListaCamposPagos);
                
              
                servicio.formatoDePagos =JsonConvert.SerializeObject(listaCampos);
               
                _dbContext.Servicio.Update(servicio);
                await _dbContext.DbContext.SaveChangesAsync();

                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AsignarServicioComandHandle.HandleAsync {Response}", servicio.Id);
                return servicio.Id;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error AgregarAdminHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw CustomException.CrearDesdeListaException(422,
                    "Se produjeron los siguientes errores de validación", ex);
            }
            catch (NullReferenceException ex)
            {
                throw new CustomException("Servicio no existente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AsignarServicioComandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }


        public void validarCamposEnlista(List<CamposPagosRequest> listaCampos)
        {
            List<ValidationFailure> listaErrores=new List<ValidationFailure>();
            foreach (var campo in listaCampos)
            {
                CamposPagosValidator Validator = new CamposPagosValidator();
                ValidationResult result = Validator.Validate(campo);
                if (!result.IsValid)
                    listaErrores.AddRange(result.Errors);

            }
            if (listaErrores.Count>0)
                throw new ValidationException(listaErrores);
            
        }





    }
}
