using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Response.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDto>> ListarClienteAsync(ClienteBuscarDto cliente);
        Task<bool> RegistrarClienteAsync(ClienteRegistrarDto request);
        Task<bool> EditarClienteAsync(ClienteEditarDto request);
        Task<bool> DarBajaClienteAsync(string ccod_coa);
    }
}
