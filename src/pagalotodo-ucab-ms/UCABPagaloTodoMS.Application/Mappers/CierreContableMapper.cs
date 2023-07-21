using System.Diagnostics.CodeAnalysis;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class CierreContableMapper
    {
        public static CierreContableResponse MapEntityAResponse(PrestadorServicioEntity prestador, ServicioEntity servicio)
        {
            CierreContableResponse response;

            if(servicio.Pago==null)
            {
                response = new CierreContableResponse()
                {
                    idPrestador = prestador.Id,
                    prestadorName = prestador.name,
                    idServicio = servicio.Id,
                    servicioName = servicio.name,
                    total=555555555555555
                };
            }
            else
            {
                response = new CierreContableResponse()
                {
                    idPrestador = prestador.Id,
                    prestadorName = prestador.name,
                    idServicio = servicio.Id,
                    servicioName = servicio.name,
                    total=servicio.Pago.Sum(p=>p.valor)
                };
                
            }


            return response;    
        }

        public static CamposConciliacionEntity MapRequestEntity(CamposConciliacionRequest request)
        {
            var entity = new CamposConciliacionEntity()
            {
               Nombre = request.Nombre,
               Longitud = request.Longitud,
               deleted = false
            };
            return entity;
        }
    }
}