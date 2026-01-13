using AutoMapper;
using Microsoft.Extensions.Configuration;
using MobilePedidos.Application.Dtos.Request.Login;
using MobilePedidos.Application.Dtos.Response.Login;
using MobilePedidos.Application.Helpers;
using MobilePedidos.Application.Interfaces;
using MobilePedidos.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MobilePedidos.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorCodigoAsync(request.Username);

            if (usuario == null) return null;

            string passwordEncriptadaApp = SecurityHelper.EncriptarContrasena(request.Password);

            if (usuario.cpassword.Trim() != passwordEncriptadaApp) return null;

            var userDto = _mapper.Map<LoginResponseDto>(usuario);
            var tokenString = GenerateJSONWebToken(userDto);
            return new TokenResponseDto
            {
                Access_Token = tokenString,
                Token_Type = "Bearer",
                Expires_Minutes = int.Parse(_configuration["DatcorpJwtAPI:ExpiresMinutes"])
            };
        }

        private string GenerateJSONWebToken(LoginResponseDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["DatcorpJwtAPI:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ccod_usu),
                new Claim(ClaimTypes.Name, user.cdsc_usu ?? ""),
                new Claim(ClaimTypes.Role, user.c_adm ?? ""), // Ajustado según tu DTO
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["DatcorpJwtAPI:Issuer"],
                audience: _configuration["DatcorpJwtAPI:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["DatcorpJwtAPI:ExpiresMinutes"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
