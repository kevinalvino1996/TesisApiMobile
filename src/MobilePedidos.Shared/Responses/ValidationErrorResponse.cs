using MobilePedidos.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Shared.Responses
{
    public class ValidationErrorResponse : GeneralResponse<object>
    {
        public List<ValidationError> Errors { get; set; } = new();
    }
}
