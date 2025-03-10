using Cliente.Aplication.Generics;
using Cliente.Aplication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cliente.Application.UserHistory.Querys.GetAllClientes.GetAllClienteHandler;
using static Cliente.Application.UserHistory.Querys.GetByEdadCliente.GetByEdadClienteHandler;
using static Cliente.Application.UserHistory.Querys.GetByIdCliente.GetByIdClienteHandler;
using static Cliente.Application.UserHistorys.Querys.GetByName.GetByNameClienteHandler;

namespace MicroServicioCliente.Controllers.V1
{
    public class ClienteQuerysController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHttp _responseHttp;

        public ClienteQuerysController(IMediator mediator, ResponseHttp responseHttp)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _responseHttp = responseHttp ?? throw new ArgumentNullException(nameof(responseHttp));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Responses<List<ClienteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllClienteQuery());
            return await _responseHttp.GetResponseHttp(response);
        }

        [HttpGet("ById{id}")]
        [ProducesResponseType(typeof(Responses<List<ClienteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetByIdClienteQuery(id));
            return await _responseHttp.GetResponseHttp(response);
        }

        [HttpGet("ByAgeMoreThan{edad}")]
        [ProducesResponseType(typeof(Responses<List<ClienteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAgeMoreThan(int edad)
        {
            var response = await _mediator.Send(new GetByEdadClienteQuery(edad));
            return await _responseHttp.GetResponseHttp(response);
        }

        [HttpGet("ByNameLike{nombre}")]
        [ProducesResponseType(typeof(Responses<List<ClienteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByNameLike(string nombre)
        {
            var response = await _mediator.Send(new GetByNameClienteQuery(nombre));
            return await _responseHttp.GetResponseHttp(response);
        }
    }
}
