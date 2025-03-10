using Cliente.Aplication.Generics;
using Cliente.Aplication.Request;
using Cliente.Aplication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cliente.Application.UserHistory.Comands.CreateCliente.CreateClienteHandler;
using static Cliente.Application.UserHistory.Comands.DeleteCliente.DeleteClienteHandler;
using static Cliente.Application.UserHistory.Comands.UpdateCliente.UpdateClienteHandler;

namespace MicroServicioCliente.Controllers.V1;

[ApiController]
[Route("api/cliente")]
public class ClienteCommandController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ResponseHttp _responseHttp;

    public ClienteCommandController(IMediator mediator,ResponseHttp responseHttp)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
        _responseHttp = responseHttp ?? throw new ArgumentNullException(nameof(responseHttp));
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] ClienteRequest clienteRequest)
    {
        if (clienteRequest == null) 
            return BadRequest();

        var response = await _mediator.Send(new CreateClienteCommand(clienteRequest));
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpPut()]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] ClienteRequest clienteRequest)
    {
        if (clienteRequest == null)
            return BadRequest();

        var response = await _mediator.Send(new UpdateClienteCommand(clienteRequest));
        return await _responseHttp.GetResponseHttp(response);
    }

    [HttpDelete()]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses<ClienteResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] ClienteRequest clienteRequest)
    {
        if (clienteRequest == null)
            return BadRequest();

        var response = await _mediator.Send(new DeleteClienteCommand(clienteRequest));
        return await _responseHttp.GetResponseHttp(response); ;
    }
}
