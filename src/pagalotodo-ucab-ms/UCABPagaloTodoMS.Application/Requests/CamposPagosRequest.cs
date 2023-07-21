using System.ComponentModel.DataAnnotations;
using UCABPagaloTodoMS.Core.Enums;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class CamposPagosRequest
    {
        public string? Nombre { get; set; } = null;
        public string? contenido { get; set; } = null;
        public int Longitud { get; set; } = 0;
        public TipoDato TipoDato { get; set; }
        public string? separadorDeMiles { get; set; } = null;
        public string? separadorDeDecimales { get; set; } = null;
        public string? formatofecha { get; set; } = null;
        public bool inCOnciliacion { get; set; } = false;

        
        
    }
}