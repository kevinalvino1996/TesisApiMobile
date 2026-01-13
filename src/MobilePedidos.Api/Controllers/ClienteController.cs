using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Response.Cliente;
using MobilePedidos.Application.Interfaces;
using MobilePedidos.Shared.Responses;

namespace MobilePedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IValidator<ClienteBuscarDto> _buscarValidator;
        private readonly IValidator<ClienteRegistrarDto> _registrarValidator;
        private readonly IValidator<ClienteEditarDto> _editarValidator;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(
            IClienteService clienteService, 
            IValidator<ClienteBuscarDto> validator, 
            IValidator<ClienteRegistrarDto> registrarValidator, 
            IValidator<ClienteEditarDto> editarValidator,
            ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _buscarValidator = validator;
            _registrarValidator = registrarValidator;
            _editarValidator = editarValidator;
            _logger = logger;
        }

        [HttpPost("Buscar")]
        public async Task<IActionResult> ListarClienteAsync([FromBody] ClienteBuscarDto request)
        {
            _logger.LogInformation("Buscando clientes con código: {Codigo}", request.Ccod_Coa);

            await _buscarValidator.ValidateAndThrowAsync(request);
            var clientes = await _clienteService.ListarClienteAsync(request);
            bool tieneDatos = clientes != null && clientes.Any();

            _logger.LogInformation("Búsqueda completada. Clientes encontrados: {Count}", clientes?.Count() ?? 0);

            var response = new GeneralResponse<IEnumerable<ClienteResponseDto>>
            {
                Success = true,
                ShowAlert = false,
                Title = "Consulta exitosa",
                Message = tieneDatos
                        ? "Clientes obtenidos correctamente"
                        : "Consulta exitosa, pero no se encontraron clientes con los criterios proporcionados",
                Data = clientes
            };
            return Ok(response);
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarClienteAsync([FromBody] ClienteRegistrarDto request)
        {
            _logger.LogInformation("Intentando registrar cliente: {Nombre}", request.cdsc_coa);

            // Validamos
            await _registrarValidator.ValidateAndThrowAsync(request);

            // Llamamos al servicio
            var result = await _clienteService.RegistrarClienteAsync(request);

            if (result)
            {
                _logger.LogInformation("Cliente registrado exitosamente: {Nombre}", request.cdsc_coa);
            }
            else
            {
                _logger.LogWarning("No se pudo registrar el cliente: {Nombre}", request.cdsc_coa);
            }

            var response = new GeneralResponse<bool>
            {
                Success = result,
                ShowAlert = true,
                Title = result ? "Registro Exitoso" : "Error",
                Message = result ? "El cliente se registró correctamente" : "No se pudo registrar al cliente",
                Data = result
            };

            return Ok(response);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> EditarClienteAsync([FromBody] ClienteEditarDto request)
        {
            _logger.LogInformation("Intentando editar cliente: {Codigo}", request.Ccod_Coa);

            // Validamos con el validador correspondiente
            await _editarValidator.ValidateAndThrowAsync(request);

            var result = await _clienteService.EditarClienteAsync(request);

            if (result)
            {
                _logger.LogInformation("Cliente editado exitosamente: {Codigo}", request.Ccod_Coa);
            }
            else
            {
                _logger.LogWarning("No se pudo editar el cliente: {Codigo}", request.Ccod_Coa);
            }

            var response = new GeneralResponse<bool>
            {
                Success = result,
                ShowAlert = true,
                Title = result ? "Actualización Exitosa" : "Atención",
                Message = result ? "El cliente se actualizó correctamente" : "No se encontró el cliente o no hubo cambios",
                Data = result
            };

            return Ok(response);
        }

        [HttpPost("Baja")]
        public async Task<IActionResult> DarBajaClienteAsync([FromBody] ClienteBuscarDto request)
        {
            _logger.LogInformation("Intentando dar de baja al cliente: {Codigo}", request.Ccod_Coa);

            // 1. Validamos el objeto usando el validador de búsqueda (ya que compartes el DTO)
            // Esto asegura que Cdsc_Coa no venga vacío
            await _buscarValidator.ValidateAndThrowAsync(request);

            var result = await _clienteService.DarBajaClienteAsync(request.Ccod_Coa);

            if (result)
            {
                _logger.LogInformation("Cliente dado de baja exitosamente: {Codigo}", request.Ccod_Coa);
            }
            else
            {
                _logger.LogWarning("No se pudo dar de baja al cliente: {Codigo}", request.Ccod_Coa);
            }

            var response = new GeneralResponse<bool>
            {
                Success = result,
                ShowAlert = true,
                Title = result ? "Operación Exitosa" : "Atención",
                Message = result ? "El cliente ha sido desactivado correctamente" : "No se pudo realizar la baja (posiblemente el código no existe)",
                Data = result
            };

            return Ok(response);
        }
    }
}
