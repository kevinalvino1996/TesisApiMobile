using MobilePedidos.Domain.Entities;

namespace MobilePedidos.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerUsuarioPorCodigoAsync(string ccod_usu);
    }
}
