using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Dtos.Request.Cliente
{
    public class ClienteEditarDto
    {
        public string Ccod_Coa { get; set; } = string.Empty; 
        public string Cdsc_Coa { get; set; } = string.Empty;
        public string Ca_Paterno { get; set; } = string.Empty;
        public string Ca_Materno { get; set; } = string.Empty;
        public string Cnombres { get; set; } = string.Empty;
        public string Cdireccion { get; set; } = string.Empty;
        public string Cmon_Cred { get; set; } = string.Empty;
        public bool Btranportista { get; set; }
    }
}
