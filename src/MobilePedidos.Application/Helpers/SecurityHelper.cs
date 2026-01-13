using System.Security.Cryptography;
using System.Text;

namespace MobilePedidos.Application.Helpers
{
    public static class SecurityHelper
    {
        private const string MyKey = "DWGTYJVMP";

        public static string EncriptarContrasena(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            // Nota: TripleDESCryptoServiceProvider y MD5 están obsoletos en .NET moderno,
            // pero se usan aquí para mantener compatibilidad con tu base de datos.
            using var des = new TripleDESCryptoServiceProvider();
            using var hashmd5 = new MD5CryptoServiceProvider();

            des.Key = hashmd5.ComputeHash(Encoding.Unicode.GetBytes(MyKey));
            des.Mode = CipherMode.ECB;

            var desEncrypt = des.CreateEncryptor();
            byte[] buff = Encoding.ASCII.GetBytes(texto);

            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buff, 0, buff.Length));
        }
    }
}
