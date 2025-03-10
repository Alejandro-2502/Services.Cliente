using Cliente.Aplication.Generics;
using Cliente.Aplication.Request;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Application.UserHistory.Comands.CreateCliente;
using MediatR;
using System.Net;
using static Cliente.Application.UserHistorys.Commands.CreateCliente.CreateValidationsClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistorys.Commands.CreateCliente
{
    public class CreateValidationsClienteHandler(CreateClienteValidator _createClienteValidator, IMediator _mediator)
        : IRequestHandler<ValidatorCreateCliente, Responses<ClienteResponse>>
    {  
        public record ValidatorCreateCliente(ClienteRequest clienteRequest) : IRequest<Responses<ClienteResponse>>;
        public async Task<Responses<ClienteResponse>> Handle(ValidatorCreateCliente command, CancellationToken cancellationToken)
        {
            try
            {
                List<string> Messages = new();
                var result = await _createClienteValidator.ValidateAsync(command.clienteRequest);

                if (!result.IsValid)
                {
                    Messages.AddRange(result.Errors.Select(error => error.ErrorMessage));
                    return Response.ErrorsList<ClienteResponse>(HttpStatusCode.BadRequest, Messages).Result;
                }

                return null;
            }
            catch (Exception ex)
            {
                await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = Enums.LogerTypes.LogerType.Error, Messages = nameof(CreateValidationsClienteHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

                return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
