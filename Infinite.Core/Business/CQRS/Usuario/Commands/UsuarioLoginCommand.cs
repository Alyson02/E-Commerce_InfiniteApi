using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Models;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Infrastructure.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Infinite.Core.Business.Services.Token;
using Infinite.Core.Business.CQRS.Usuario.Validations;

namespace Infinite.Core.Business.CQRS.Usuario.Commands
{

    public class UsuarioLoginCommand : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class UsuarioLoginCommandHandler : IRequestHandler<UsuarioLoginCommand, Response>
        {
            private readonly IServiceBase<UsuarioEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            private readonly ITokenService _tokenService;
            public UsuarioLoginCommandHandler(IServiceBase<UsuarioEntity> service, 
                                              ITokenService tokenService,
                                              IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _tokenService = tokenService;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(UsuarioLoginCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service
                        .CreateSpec(x => x.Email == command.Email && 
                        x.Password == command.Password)
                        .AddInclude(x => x.TipoUsuario);
                    var user = await _service.FindAsync(spec);

                    var specCliente = _clienteService.CreateSpec(x => x.UsuarioId == user.UserId);
                    var cliente = await _clienteService.FindAsync(specCliente);

                    if (user is null) throw new Exception("Email e/ou senha inválido(s)");

                    var token = _tokenService.GerarToken(cliente, user);

                    return new Response(token);
                }
                catch (ValidationException ve)
                {
                    throw new ValidationException(ve.Message);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer login", e);
                }
            }
        }
    }
}
