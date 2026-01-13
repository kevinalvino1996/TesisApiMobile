using AutoMapper;
using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Response.Cliente;
using MobilePedidos.Application.Interfaces;
using MobilePedidos.Domain.Entities;
using MobilePedidos.Domain.Interfaces;

namespace MobilePedidos.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteResponseDto>> ListarClienteAsync(ClienteBuscarDto request)
        {
            var clientes = await _clienteRepository.ListarClienteAsync(request.Cdsc_Coa);
            return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
        }

        public async Task<bool> RegistrarClienteAsync(ClienteRegistrarDto request)
        {
            // Mapeamos el DTO a la Entidad de Dominio
            var clienteEntity = _mapper.Map<Cliente>(request);
            return await _clienteRepository.RegistrarClienteAsync(clienteEntity);
        }

        public async Task<bool> EditarClienteAsync(ClienteEditarDto request)
        {
            // Mapeamos el DTO a la Entidad de Dominio
            var clienteEntity = _mapper.Map<Cliente>(request);
            return await _clienteRepository.EditarClienteAsync(clienteEntity);
        }

        public async Task<bool> DarBajaClienteAsync(string ccod_coa)
        {
            // Aquí podrías agregar lógica de negocio, como verificar si el cliente tiene deudas
            return await _clienteRepository.DarBajaClienteAsync(ccod_coa);
        }
    }
}
