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
using static Cliente.Application.UserHistory.Comands.DeleteCliente.DeleteClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Comands.DeleteCliente;

public class DeleteClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    : IRequestHandler<DeleteClienteCommand, Responses<ClienteResponse>>
{
    public record DeleteClienteCommand(ClienteRequest clienteRequest) : IRequest<Responses<ClienteResponse>>;
    public async Task<Responses<ClienteResponse>> Handle(DeleteClienteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            
            var clienteEntity = _mapper.Map<ClienteEntity>(command.clienteRequest);

            var result = await _unitOfWork.clienteCommandRepository.DeleteAsync(clienteEntity);
            
            if (result is false)
                return await Response.Error<ClienteResponse>(HttpStatusCode.Conflict, Messages.MessagesCliente.DeleteClienteConflic);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = LogerType.Information, Messages = nameof(DeleteClienteHandler) + " - " + nameof(Handle) + "- OK" }), cancellationToken);

            var response = _mapper.Map<ClienteResponse>(result);
            
           return await Response.Ok<ClienteResponse>(HttpStatusCode.OK, Messages.MessagesCliente.DeleteClienteOk);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
                  new LogsRegister { Type = LogerType.Error, Messages = nameof(DeleteClienteHandler) + " - " + nameof(Handle)+ ex.ToString() }), cancellationToken);
            return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
