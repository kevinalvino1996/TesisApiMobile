using FluentValidation;
using MobilePedidos.Application.Dtos.Request.Cliente;

namespace MobilePedidos.Application.Validators.Cliente
{
    public class ClienteRegistrarDtoValidator : AbstractValidator<ClienteRegistrarDto>
    {
        public ClienteRegistrarDtoValidator()
        {
            RuleFor(x => x.ccod_coa).NotEmpty().MaximumLength(11);
            RuleFor(x => x.cdsc_coa).NotEmpty().MaximumLength(100);
            RuleFor(x => x.cnombres).NotEmpty().MaximumLength(50);

        }
    }
}
