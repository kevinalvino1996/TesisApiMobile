using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Domain.Entities
{
    public class Usuario
    {
        public string ccod_usu { get; set; } = string.Empty;
        public string cdsc_usu { get; set; } = string.Empty;
        public string c_adm { get; set; } = string.Empty;
        public string cmail { get; set; } = string.Empty;
        public string cpassword { get; set; } = string.Empty;
        public string cstatus { get; set; } = string.Empty;
    }
}
