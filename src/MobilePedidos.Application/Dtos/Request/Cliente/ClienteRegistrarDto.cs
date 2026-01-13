using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Dtos.Request.Cliente
{
    public class ClienteRegistrarDto
    {
        public string ccod_coa { get; set; } = string.Empty;
        public string cdsc_coa { get; set; } = string.Empty;
        public string ca_paterno { get; set; } = string.Empty;
        public string ca_materno { get; set; } = string.Empty;
        public string cnombres { get; set; } = string.Empty;
        public string cdireccion { get; set; } = string.Empty;
        public string cmon_cred { get; set; } = string.Empty;
        public bool btranportista { get; set; }
    }
}
