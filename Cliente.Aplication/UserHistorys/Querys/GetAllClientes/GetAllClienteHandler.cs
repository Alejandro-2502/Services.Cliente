using AutoMapper;
using Cliente.Aplication.Generics;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Domain.Interfaces;
using MediatR;
using System.Net;
using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistory.Querys.GetAllClientes.GetAllClienteHandler;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistory.Querys.GetAllClientes
{
    public class GetAllClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetAllClienteQuery, Responses<List<ClienteResponse>>>
    {
        public record GetAllClienteQuery() : IRequest<Responses<List<ClienteResponse>>>;
        public async Task<Responses<List<ClienteResponse>>> Handle(GetAllClienteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork.clienteQuerysRepository.GetAllAsync();

                if (!results.Any())
                    return await Response.Error<List<ClienteResponse>>(HttpStatusCode.NotFound, Messages.MessagesCliente.GetAllClienteConflic);

                var response = _mapper.Map<List<ClienteResponse>>(results);
                return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new RegisterLogCommand(
                    new LogsRegister { Type = LogerType.Error, Messages = nameof(GetAllClienteHandler) + " - " + nameof(Handle) + ex.ToString() }), cancellationToken);
                return await Response.Error<List<ClienteResponse>>(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
}
