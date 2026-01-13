using MobilePedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarClienteAsync(string buscarCliente);
        Task<bool> RegistrarClienteAsync(Cliente cliente);
        Task<bool> EditarClienteAsync(Cliente cliente);
        Task<bool> DarBajaClienteAsync(string ccod_coa);
    }
}
