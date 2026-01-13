using FluentValidation;
using MobilePedidos.Application.Dtos.Request.Cliente;

namespace MobilePedidos.Application.Validators.Cliente
{
    public class ClienteEditarDtoValidator : AbstractValidator<ClienteEditarDto>
    {
        public ClienteEditarDtoValidator()
        {
            RuleFor(x => x.Ccod_Coa).NotEmpty().WithMessage("El código es obligatorio");
            RuleFor(x => x.Cdsc_Coa).NotEmpty().MaximumLength(100);

        }
    }
}
