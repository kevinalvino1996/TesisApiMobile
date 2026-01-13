using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Shared.Errors
{
    public class ValidationError
    {
        public string Field { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }
}
