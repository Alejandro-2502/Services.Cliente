using AutoMapper;
using Cliente.Aplication.Generics;
using Cliente.Aplication.Responses;
using Cliente.Application.Common.Logger;
using Cliente.Application.Common.Response;
using Cliente.Domain.Interfaces;
using MediatR;
using System.Net;
using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;
using static Cliente.Application.UserHistorys.Querys.GetByName.GetByNameClienteHandler;

namespace Cliente.Application.UserHistorys.Querys.GetByName;
public class GetByNameClienteHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IMediator _mediator)
       : IRequestHandler<GetByNameClienteQuery, Responses<List<ClienteResponse>>>
    {
        public record GetByNameClienteQuery(string name) : IRequest<Responses<List<ClienteResponse>>>;
        public async Task<Responses<List<ClienteResponse>>> Handle(GetByNameClienteQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.clienteQuerysRepository.GetByNameLike(command.name);

                if (result.Count == 0)
                    return await Response.Error<List<ClienteResponse>>(HttpStatusCode.NotFound, Messages.MessagesCliente.GetByNameLikeClienteNotFound);

                var response = _mapper.Map<List<ClienteResponse>>(result);

                return await Response.Ok(HttpStatusCode.OK, string.Empty, response);
            }
            catch (Exception ex)
            {
            await _mediator.Send(new RegisterLogCommand(
               new LogsRegister { Type = LogerType.Error, Messages = nameof(GetByNameClienteQuery).ToString() + " - " + nameof(Handle).ToString() + ex.ToString() }), cancellationToken);
            return await Response.Error<List<ClienteResponse>>(HttpStatusCode.InternalServerError, ex.Message);

        }
    }
    }
