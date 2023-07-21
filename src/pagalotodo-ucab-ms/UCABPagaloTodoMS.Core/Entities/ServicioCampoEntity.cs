using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UCABPagaloTodoMS.Core.Entities
{
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