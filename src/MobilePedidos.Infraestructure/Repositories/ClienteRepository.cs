using Dapper;
using MobilePedidos.Domain.Entities;
using MobilePedidos.Domain.Interfaces;
using MobilePedidos.Infraestructure.DbConnection;
using System.Data;

namespace MobilePedidos.Infraestructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DapperContext _context;
        public ClienteRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cliente>> ListarClienteAsync(string buscarCliente)
        {
            using var connection = _context.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@buscarCliente", buscarCliente);

            var clientes = await connection.QueryAsync<Cliente>(
                "uspClienteBuscar",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return clientes;
        }

        public async Task<bool> RegistrarClienteAsync(Cliente cliente)
        {
            using var connection = _context.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@ccod_coa", cliente.ccod_coa);
            parametros.Add("@cdsc_coa", cliente.cdsc_coa);
            parametros.Add("@ca_paterno", cliente.ca_paterno);
            parametros.Add("@ca_materno", cliente.ca_materno);
            parametros.Add("@cnombres", cliente.cnombres);
            parametros.Add("@cdireccion", cliente.cdireccion);
            parametros.Add("@cmon_cred", cliente.cmon_cred);
            parametros.Add("@btranportista", cliente.btranportista);

            // ExecuteAsync devuelve el número de filas afectadas
            var result = await connection.ExecuteAsync(
                "uspClienteRegistrar",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        public async Task<bool> EditarClienteAsync(Cliente cliente)
        {
            using var connection = _context.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@ccod_coa", cliente.ccod_coa);
            parametros.Add("@cdsc_coa", cliente.cdsc_coa);
            parametros.Add("@ca_paterno", cliente.ca_paterno);
            parametros.Add("@ca_materno", cliente.ca_materno);
            parametros.Add("@cnombres", cliente.cnombres);
            parametros.Add("@cdireccion", cliente.cdireccion);
            parametros.Add("@cmon_cred", cliente.cmon_cred);
            parametros.Add("@btranportista", cliente.btranportista);

            var result = await connection.ExecuteAsync(
                "uspClienteEditar",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result > 0; // Retorna true si se actualizó al menos una fila
        }

        public async Task<bool> DarBajaClienteAsync(string ccod_coa)
        {
            using var connection = _context.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@ccod_coa", ccod_coa);

            var result = await connection.ExecuteAsync(
                "uspClienteBaja",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }
    }
}
