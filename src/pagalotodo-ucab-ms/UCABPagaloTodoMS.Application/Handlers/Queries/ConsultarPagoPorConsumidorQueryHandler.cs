﻿using UCABPagaloTodoMS.Core.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.EntityFrameworkCore;
using UCABPagaloTodoMS.Application.CustomExceptions;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class ConsultarPagoPorConsumidorQueryHandler : IRequestHandler<ConsultarPagoPorConsumidorQuery, List<PagoResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<ConsultarPagoPorConsumidorQueryHandler> _logger;

        public ConsultarPagoPorConsumidorQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<ConsultarPagoPorConsumidorQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<PagoResponse>> Handle(ConsultarPagoPorConsumidorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarPagoQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }

                return HandleAsync(request);
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
                _logger.LogWarning("ConsultarPagoQueryHandler.Handle: ArgumentNullException");
                throw new CustomException(ex.Message);
            }
        }

        private async Task<List<PagoResponse>> HandleAsync(ConsultarPagoPorConsumidorQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarPagoQueryHandler.HandleAsync");

                // Consulta los registros de la tabla Pago que correspondan al consumidor especificado en la consulta
                var result = _dbContext.Pago
                    .Where(c => c.consumidor.Id == request.IdConsumidor &&c.deleted==false)
                    .Select(c => new PagoResponse()
                    {
                        Id = c.Id,
                        valor = c.valor,
                        consumidorId = c.consumidor.Id,
                        servicioId = c.servicio.Id,
                        PrestadorServicioNombre = c.servicio.PrestadorServicio.name,
                        NombreServicio = c.servicio.name,
                        NombreConsumidor = c.consumidor.name
                    });

                // Ejecuta la consulta y devuelve los resultados como una lista
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarPagoQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw new CustomException("Error en la consulta", ex);
            }
        }
    }
}
