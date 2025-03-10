using Cliente.Aplication.Generics;
using Cliente.Aplication.Request;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Application.UserHistory.Comands.UpdateCliente;
using MediatR;
using System.Net;
using static Cliente.Application.UserHistorys.Commands.UpdateCliente.UpdateValidationsClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistorys.Commands.UpdateCliente
{
    public class UpdateValidationsClienteHandler(UpdateClienteValidator _updateClienteValidator, IMediator _mediator)
         : IRequestHandler<ValidatorUpDateCliente, Responses<ClienteResponse>>
    {
        public record ValidatorUpDateCliente(ClienteRequest clienteRequest) : IRequest<Responses<ClienteResponse>>;
        public async Task<Responses<ClienteResponse>> Handle(ValidatorUpDateCliente command, CancellationToken cancellationToken)
        {
            try
            {
                List<string> Messages = new();
                var result = await _updateClienteValidator.ValidateAsync(command.clienteRequest);

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
                    new LogsRegister { Type = Enums.LogerTypes.LogerType.Error, Messages = nameof(UpdateValidationsClienteHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

                return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
