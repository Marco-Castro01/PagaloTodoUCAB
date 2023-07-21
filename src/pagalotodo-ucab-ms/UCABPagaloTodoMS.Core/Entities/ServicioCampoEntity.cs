using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class ServicioCampoEntity 
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        [ForeignKey(nameof(Servicio))]
        [Key]
        public Guid ServicioId { get; set; }
        public ServicioEntity Servicio { get; set; }

        [ForeignKey(nameof(Campo))]
        [Key]
        public Guid CampoId { get; set; }
        public CamposConciliacionEntity Campo { get; set; }
    }
}