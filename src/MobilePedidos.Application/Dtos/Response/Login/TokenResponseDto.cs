using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Dtos.Response.Login
{
    public class TokenResponseDto
    {
        public string Access_Token { get; set; } = string.Empty;
        public string Token_Type { get; set; } = string.Empty;
        public int Expires_Minutes { get; set; }
    }
}
