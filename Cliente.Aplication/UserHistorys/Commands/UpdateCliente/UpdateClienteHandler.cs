using AutoMapper;
using Cliente.Aplication.Generics;
using Cliente.Aplication.Request;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Domain.Entitys;
using Cliente.Domain.Interfaces;
using MediatR;
using System.Net;
using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistory.Comands.UpdateCliente.UpdateClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Comands.UpdateCliente;

public class UpdateClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    : IRequestHandler<UpdateClienteCommand, Responses<ClienteResponse>>
{
    public record UpdateClienteCommand(ClienteRequest clienteRequest) : IRequest<Responses<ClienteResponse>>;
    public async Task<Responses<ClienteResponse>> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
    {
		try
		{
            var clienteEntity = _mapper.Map<ClienteEntity>(command.clienteRequest);

            var result = await _unitOfWork.clienteCommandRepository.UpdateAsync(clienteEntity);

            if (result is null)
                return await Response.Error<ClienteResponse>(HttpStatusCode.Conflict, Messages.MessagesCliente.UpDateClienteConflict);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Send(new RegisterLogCommand(
                   new LogsRegister { Type = LogerType.Information, Messages = nameof(UpdateClienteHandler).ToString() + " - " + nameof(Handle).ToString() + "- OK" }), cancellationToken);

            var response = _mapper.Map<ClienteResponse>(result);

            return await Response.Ok<ClienteResponse>(HttpStatusCode.OK, Messages.MessagesCliente.UpDateClienteOk);
        }
		catch (Exception ex)
		{
            await _mediator.Send(new RegisterLogCommand(
                   new LogsRegister { Type = LogerType.Error, Messages = nameof(UpdateClienteHandler).ToString() + " - " + nameof(Handle).ToString() + ex.ToString() }), cancellationToken);
            return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}



