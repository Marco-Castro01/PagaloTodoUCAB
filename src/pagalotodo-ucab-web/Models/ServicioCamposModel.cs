
using UCABPagaloTodoWeb.enums;

namespace UCABPagaloTodoWeb.Models
{
    public class ServicioCamposModel
    {
        public ServicioModel servicio { get; set; }
        public List<CamposConciliacionModel> camposConciliacion { get; set; } 
        
    }
}