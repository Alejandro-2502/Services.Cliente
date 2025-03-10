using AutoMapper;
using Cliente.Aplication.DTOs;
using Cliente.Aplication.Request;
using Cliente.Aplication.Responses;
using Cliente.Domain.Entitys;

namespace Cliente.Aplication.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ClienteRequest, ClienteDTO>();
        CreateMap<ClienteDTO,ClienteEntity>();
        CreateMap<ClienteEntity, ClienteRequest>();
        CreateMap<ClienteRequest,ClienteEntity>();
        CreateMap<ClienteEntity, ClienteResponse>();
    }
}
