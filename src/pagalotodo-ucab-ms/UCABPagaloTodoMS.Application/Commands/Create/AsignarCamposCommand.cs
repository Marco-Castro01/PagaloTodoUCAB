using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AsignarCamposCommand : IRequest<List<ServicioCampoEntity>>
    {
        public Guid _servicioId { get; set; }
        public CamposAsigRequest _request { get; set; }

        public AsignarCamposCommand(Guid servicioId, CamposAsigRequest camposAsigRequest)
        {
            _request = camposAsigRequest;
            _servicioId = servicioId;
        }
    }
  
}
