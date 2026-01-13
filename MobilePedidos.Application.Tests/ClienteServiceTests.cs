using AutoMapper;
using FluentAssertions;
using MobilePedidos.Application.Dtos.Request.Cliente;
using MobilePedidos.Application.Dtos.Response.Cliente;
using MobilePedidos.Application.Services;
using MobilePedidos.Domain.Entities;
using MobilePedidos.Domain.Interfaces;
using Moq;

namespace MobilePedidos.Application.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClienteService _sut; // System Under Test

        public ClienteServiceTests()
        {
            _mockRepository = new Mock<IClienteRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new ClienteService(_mockRepository.Object, _mockMapper.Object);
        }

        //Marca un test unitario único (sin parámetros)
        [Fact]
        public async Task ListarClienteAsync_DeberiaRetornarClientes_CuandoExistenDatos()
        {
            // Arrange
            var buscarDto = new ClienteBuscarDto { Cdsc_Coa = "CLI001" };
            var clientesEntidad = new List<Cliente>
            {
                new Cliente { ccod_coa = "CLI001", cdsc_coa = "Cliente Test" }
            };
            var clientesResponse = new List<ClienteResponseDto>
            {
                new ClienteResponseDto { ccod_coa = "CLI001", cdsc_coa = "Cliente Test" }
            };

            //Cuando el servicio llame a ListarClienteAsync("CLI001"), devuelve esta lista falsa clientesEntidad
            _mockRepository
                .Setup(r => r.ListarClienteAsync(buscarDto.Cdsc_Coa))
                .ReturnsAsync(clientesEntidad);

            //Simula el AutoMapper
            _mockMapper
                .Setup(m => m.Map<IEnumerable<ClienteResponseDto>>(clientesEntidad))
                .Returns(clientesResponse);

            // Act
            // Ejecutas el método real del servicio
            var resultado = await _sut.ListarClienteAsync(buscarDto);

            // Assert
            //No está vacío
            resultado.Should().NotBeEmpty();
            //Tiene 1 elemento
            resultado.Should().HaveCount(1);
            //El código es correcto
            resultado.First().ccod_coa.Should().Be("CLI001");

            //Verifica que el repositorio fue llamado exactamente una vez
            _mockRepository.Verify(r => r.ListarClienteAsync(buscarDto.Cdsc_Coa), Times.Once);
        }

        //Test con múltiples valores
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ListarClienteAsync_DeberiaRetornarVacio_CuandoNoHayCoincidencias(string criterio)
        {
            // Arrange
            var buscarDto = new ClienteBuscarDto { Cdsc_Coa = criterio };
            
            _mockRepository
                .Setup(r => r.ListarClienteAsync(criterio))
                .ReturnsAsync(new List<Cliente>());

            // Act
            
            var resultado = await _sut.ListarClienteAsync(buscarDto);

            // Assert
            //El servicio debe responder con lista vacía
            resultado.Should().BeEmpty();
        }

        [Fact]
        public async Task RegistrarClienteAsync_DeberiaRetornarTrue_CuandoRegistroExitoso()
        {
            // Arrange
            var registrarDto = new ClienteRegistrarDto
            {
                ccod_coa = "CLI002",
                cdsc_coa = "Nuevo Cliente"
            };
            var clienteEntity = new Cliente
            {
                ccod_coa = "CLI002",
                cdsc_coa = "Nuevo Cliente"
            };

            _mockMapper.Setup(m => m.Map<Cliente>(registrarDto)).Returns(clienteEntity);

            //Simula inserción exitosa
            _mockRepository.Setup(r => r.RegistrarClienteAsync(clienteEntity)).ReturnsAsync(true);

            // Act
            var resultado = await _sut.RegistrarClienteAsync(registrarDto);

            // Assert
            //El servicio responde correctamente
            resultado.Should().BeTrue();
            _mockRepository.Verify(r => r.RegistrarClienteAsync(It.IsAny<Cliente>()), Times.Once);
        }



        //Moq → simula dependencias
        //AutoMapper → convierte objetos
        //FluentAssertions → valida resultados
        //xUnit → ejecuta los tests
        //ClienteServiceTests → garantiza que tu lógica funciona





    }
}
