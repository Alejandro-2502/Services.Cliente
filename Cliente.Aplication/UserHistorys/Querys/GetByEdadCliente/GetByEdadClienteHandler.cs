using AutoMapper;
using Cliente.Aplication.Generics;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Domain.Interfaces;
using MediatR;
using System.Net;
using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistory.Querys.GetByEdadCliente.GetByEdadClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Querys.GetByEdadCliente;

public class GetByEdadClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
    :IRequestHandler<GetByEdadClienteQuery, Responses<List<ClienteResponse>>>
{
    public record GetByEdadClienteQuery(int edad) : IRequest<Responses<List<ClienteResponse>>>;
    public async Task<Responses<List<ClienteResponse>>> Handle(GetByEdadClienteQuery command, CancellationToken cancellationToken)
    {
        try
		{
            var result = await _unitOfWork.clienteQuerysRepository.GetByAgeMoreThan(command.edad);

            if (result is null)
                return await Response.Error<List<ClienteResponse>>(HttpStatusCode.NotFound, Messages.MessagesCliente.GetByIdClienteNotFound);

            var response = _mapper.Map<List<ClienteResponse>>(result);
            return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
        }
		catch (Exception ex)
		{
            await _mediator.Send(new RegisterLogCommand(
                   new LogsRegister { Type = LogerType.Error, Messages = nameof(GetByEdadClienteQuery).ToString() + " - " + nameof(Handle).ToString() + ex.ToString() }), cancellationToken);
            return await Response.Error<List<ClienteResponse>>(HttpStatusCode.InternalServerError, ex.Message);
        }   
    }
}
