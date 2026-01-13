using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Shared.Responses
{
    public class LoginErrorResponse
    {
        public string error { get; set; } = string.Empty;
        public string error_description { get; set; } = string.Empty;
    }
}
