using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Dtos.Response.Login
{
    public class LoginResponseDto
    {
        public string ccod_usu { get; set; } = string.Empty;
        public string cdsc_usu { get; set; } = string.Empty;
        public string cpassword { get; set; } = string.Empty;
        public string c_adm { get; set; } = string.Empty;
    }
}
