using FluentValidation;
using MobilePedidos.Application.Dtos.Request.Cliente;

namespace MobilePedidos.Application.Validators.Cliente
{
    public class ClienteBuscarDtoValidator : AbstractValidator<ClienteBuscarDto>
    {
        public ClienteBuscarDtoValidator()
        {
            RuleFor(x => x.Ccod_Coa)
                .MaximumLength(11).WithMessage("El campo Cdsc_ Coa como Máximo 11 caracteres");

            RuleFor(x => x.Cdsc_Coa)
                .MaximumLength(10).WithMessage("El campo Cdsc_ Coa como Máximo 100 caracteres");
        }
    }
}
