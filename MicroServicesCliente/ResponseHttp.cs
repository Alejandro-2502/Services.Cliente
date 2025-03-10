using Cliente.Aplication.Generics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroServicioCliente;

public class ResponseHttp: ControllerBase
{
    public async Task<IActionResult> GetResponseHttp<T>(Responses<T> response)
    {
        if (response.StatusCode is HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);

                case HttpStatusCode.NotFound:
                    return NotFound(response);

                case HttpStatusCode.Conflict:
                    return Conflict(response);

                case HttpStatusCode.InternalServerError:
                    return StatusCode((int)HttpStatusCode.InternalServerError, response);

                case HttpStatusCode.BadRequest:
                    return BadRequest(response);

                default:
                    return BadRequest("Estado HTTPs no manejado explícitamente");
            }
        }

        return BadRequest("El tipo de respuesta no contiene un código de estado válido.");
    }
}
