﻿using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class PagoResponse
    {
        public Guid Id { get; set; }
        public double? valor { get; set; }
        public StatusPago? statusPago { get; set; }  
        public Guid servicioId { get; set; }
        public string? NombreServicio { get; set; }
        public string? PrestadorServicioNombre { get; set; }
        public Guid consumidorId { get; set; }
        public string NombreConsumidor { get; set; }
    }
}
