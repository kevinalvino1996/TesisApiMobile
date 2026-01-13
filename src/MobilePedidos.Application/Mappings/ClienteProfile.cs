using AutoMapper;
using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Response.Cliente;
using MobilePedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteResponseDto>();
            CreateMap<ClienteRegistrarDto, Cliente>();
            CreateMap<ClienteEditarDto, Cliente>();
        }
    }
}
