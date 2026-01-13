using FluentValidation;
using MobilePedidos.Application.Dtos.Request.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePedidos.Application.Validators.Login
{
    public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El Username es obligatorio")
                .MaximumLength(3).WithMessage("El campo Username como Máximo 3 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("El Password es obligatorio");

        }
    }
}
