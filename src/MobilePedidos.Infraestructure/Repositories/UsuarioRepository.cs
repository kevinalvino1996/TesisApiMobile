using Dapper;
using MobilePedidos.Domain.Entities;
using MobilePedidos.Domain.Interfaces;
using MobilePedidos.Infraestructure.DbConnection;
using System.Data;

namespace MobilePedidos.Infraestructure.Repositories
{
    public class UsuarioRepository  : IUsuarioRepository
    {
        private readonly DapperContext _context;
        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> ObtenerUsuarioPorCodigoAsync(string ccod_usu)
        {
            using var connection = _context.CreateConnection();
            var parametros = new DynamicParameters();
            parametros.Add("@ccod_usu", ccod_usu);

            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                "uspUsuarioObtener",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
