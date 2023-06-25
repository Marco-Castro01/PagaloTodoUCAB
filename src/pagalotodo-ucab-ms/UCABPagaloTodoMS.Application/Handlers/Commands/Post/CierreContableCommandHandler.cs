using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Firebase.Auth.Providers;
using Firebase.Storage;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.CustomExceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Validators;
using UCABPagaloTodoMS.Core.Database;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;
using UCABPagaloTodoMS.Infrastructure.Migrations;
using ValidationException = FluentValidation.ValidationException;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    /// <summary>
    /// Clase que maneja el comando para agregar un prestador.
    /// </summary>
    public class CierreContableCommandHandler : IRequestHandler<CierreContableCommand, List<CierreContableResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<CierreContableCommandHandler> _logger;
        private readonly IMediator _mediator;
 
        /// <summary>
        /// Constructor de la clase AgregarPrestadorCommandHandler.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos</param>
        /// <param name="logger">Instancia de ILogger</param>
        public CierreContableCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<CierreContableCommandHandler> logger,IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;

        }

        /// <summary>
        /// Maneja el comando para agregar un prestador.
        /// </summary>
        /// <param name="request">Comando para agregar un prestador</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Identificador del prestador agregado</returns>
        public async Task<List<CierreContableResponse>> Handle(CierreContableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleAsync(request._idprestadorservicio);
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
        /// Maneja asincrónicamente el comando para agregar un prestador.
        /// </summary>
        /// <param name="request">Comando para agregar un prestador</param>
        /// <returns>Identificador del prestador agregado</returns>
        /// 
        [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command", MessageId = "time: 1836ms")]
        [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command", MessageId = "time: 8698ms")]
        private async Task<List<CierreContableResponse>> HandleAsync(Guid request)
        {
            try
            {
                _logger.LogInformation("AgregarPrestadorCommandHandler.HandleAsync {Request}", request);
                var prestadorServicio = _dbContext.PrestadorServicio.FirstOrDefault(c => c.Id == request);
                var servicios = _dbContext.Servicio
                    .Where(c => c.PrestadorServicio.Id == prestadorServicio.Id).Include(p=>p.Pago).ToList();
                var ultimoCierreFecha = await _dbContext.ArchivoFirebase
                    .Where(l => l.prestadorServicio.Id == prestadorServicio.Id)
                    .OrderByDescending(p => p.CreatedAt)
                    .Select(p => p.CreatedAt)
                    .FirstOrDefaultAsync();

                if (ultimoCierreFecha == default)
                    ultimoCierreFecha = new DateTime(1000, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Establecer el 1 de enero de 2000 como fecha por defecto

                List<CierreContableResponse> listaCierre = new List<CierreContableResponse>();
                foreach (var servicio in servicios)
                {
                    
                    if(servicio.Pago != null)
                        servicio.Pago=servicio.Pago.Where(p => p.CreatedAt > ultimoCierreFecha).ToList();
                    listaCierre.Add(CierreContableMapper.MapEntityAResponse(prestadorServicio,servicio));
                }
                

                var query =new CrearYEnviarArchivoConciliacionCommand(request);
                var response = await _mediator.Send(query);
                listaCierre.ForEach(objeto => objeto.archivoURL= response);

                return listaCierre;
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error AgregarPrestadorCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarPrestadorCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException(ex.Message);
            }

        }
    }

    
}
