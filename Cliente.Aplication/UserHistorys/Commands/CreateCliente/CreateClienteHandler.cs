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
using static Cliente.Application.UserHistory.Comands.CreateCliente.CreateClienteHandler;
using static Cliente.Application.UserHistorys.Commands.CreateCliente.CreateValidationsClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Comands.CreateCliente;

public class CreateClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator) 
    : IRequestHandler<CreateClienteCommand, Responses<ClienteResponse>>
{
    public record CreateClienteCommand(ClienteRequest clienteRequest) : IRequest<Responses<ClienteResponse>>;  
    public async Task<Responses<ClienteResponse>> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var resultValidator = await _mediator.Send(new ValidatorCreateCliente(command.clienteRequest), cancellationToken);

            if (resultValidator is not null)
                return resultValidator; 

            var clienteEntity = _mapper.Map<ClienteEntity>(command.clienteRequest);

            var result = await _unitOfWork.clienteCommandRepository.AddAsync(clienteEntity);

            if (result is null)
                return await Response.Error<ClienteResponse>(HttpStatusCode.Conflict, Messages.MessagesCliente.CreateClienteConflict);

            await _unitOfWork.SaveChangesAsync();

            await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = LogerType.Information, Messages = nameof(CreateClienteHandler) + " - " + nameof(Handle) + "- OK" }), cancellationToken);


            var response = _mapper.Map<ClienteResponse>(result);

          return await Response.Ok<ClienteResponse>(HttpStatusCode.OK, Messages.MessagesCliente.CreateClienteOk);
        }
        catch (Exception ex)
        {
            await _mediator.Send(new RegisterLogCommand(
                new LogsRegister { Type = LogerType.Error, Messages = nameof(CreateClienteHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

            return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
