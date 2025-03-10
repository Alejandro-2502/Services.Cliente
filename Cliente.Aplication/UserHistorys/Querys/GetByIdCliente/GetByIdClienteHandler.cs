using AutoMapper;
using Cliente.Aplication.Generics;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Domain.Interfaces;
using MediatR;
using System.Net;
using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistory.Querys.GetByIdCliente.GetByIdClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Querys.GetByIdCliente
{
    public class GetByIdClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetByIdClienteQuery, Responses<ClienteResponse>>
    {
        public record GetByIdClienteQuery(int id) : IRequest<Responses<ClienteResponse>>;
        public async Task<Responses<ClienteResponse>> Handle(GetByIdClienteQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.clienteQuerysRepository.GetAsync(command.id);

                if (result is null)
                    return await Response.Error<ClienteResponse>(HttpStatusCode.NotFound, Messages.MessagesCliente.GetByIdClienteNotFound);

                var response = _mapper.Map<ClienteResponse>(result);

                return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new RegisterLogCommand(
                   new LogsRegister { Type = LogerType.Error, Messages = nameof(GetByIdClienteHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);

                return await Response.Error<ClienteResponse>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
