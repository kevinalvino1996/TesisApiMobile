using AutoMapper;
using MobilePedidos.Application.Dtos.Response.Login;
using MobilePedidos.Domain.Entities;

namespace MobilePedidos.Application.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, LoginResponseDto>();
        }
    }
}
