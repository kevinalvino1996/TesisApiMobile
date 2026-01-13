using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Request.Login;
using MobilePedidos.Application.Dtos.Response.Login;
using MobilePedidos.Application.Interfaces;
using MobilePedidos.Shared.Responses;

namespace MobilePedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginRequestDto> _loginValidator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, IValidator<LoginRequestDto> validator, ILogger<AuthController> logger)
        {
            _authService = authService;
            _loginValidator = validator;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            await _loginValidator.ValidateAndThrowAsync(request);
            _logger.LogInformation("Intento de login desde Client_Url: {ClientUrl}, Usuario: {Username}", request.Client_Url, request.Username);

            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new LoginErrorResponse
                {
                    error = "invalid_grant",
                    error_description = "Usuario o contraseña incorrectos"
                });
            }
            return Ok(result);
        }
    }
}
