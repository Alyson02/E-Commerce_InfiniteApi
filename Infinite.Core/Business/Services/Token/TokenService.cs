using Infinite.Core.Domain.Entities;
using Infinite.Core.Business.Services.Token;
using Infinite.Core.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UsuarioToken GerarToken(ClienteEntity cliente, UsuarioEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
            var expiration = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JWT:Expiration"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    user.TipoUsuarioId == 2 ? new Claim(ClaimTypes.Name, cliente.Nome) :  
                    new Claim(ClaimTypes.Name, "funcionario"),
                    new Claim("uid", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.TipoUsuario.Role.ToString())
                }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UsuarioToken()
            {
                Token = tokenHandler.WriteToken(token),
                Expiracao = expiration,
                Role = user.TipoUsuario.Role,
                Nome = user.TipoUsuarioId == 2 ? cliente.Nome : "Funcionario"
            };
        }
    }
}
