using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Shared.Responses
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public bool ShowAlert { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public T? Data { get; set; } = default;
    }
}
