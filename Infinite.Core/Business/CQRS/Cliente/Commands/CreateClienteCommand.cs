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
using Infinite.Core.Business.CQRS.Cliente.Validations;

namespace Infinite.Core.Business.CQRS.Cliente.Commands
{ //colocar o namespace correto Ex.: .Produto.Commands

    public class CreateClienteCommand : IRequest<Response>
    {
        //Props
        public string Nome { get; set; }
        public string Tell { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Response>
        {
            private readonly IServiceBase<ClienteEntity> _service;
            public CreateClienteCommandHandler(IServiceBase<ClienteEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var clienteValidator = new CreateClienteValidator(_service);
                    var resClienteValidator = await clienteValidator.ValidateAsync(command);

                    if (!resClienteValidator.IsValid) throw new ValidationException(resClienteValidator.Errors);

                    var cliente = new ClienteEntity
                    {
                        Nome = command.Nome,
                        Tell = command.Tell,
                        Pontos = 0,
                        Usuario = new UsuarioEntity
                        {
                            Email = command.Email,
                            Password = command.Senha,
                            TipoUsuarioId = (int)TipoUsuarioEnum.USUARIO_SITE
                        }
                    };

                    await _service.InsertAsync(cliente);

                    return new Response("Cliente adicionado com sucesso");
                }
                catch(ValidationException ve)
                {
                    throw new ValidationException(ve.Message);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Cliente", e);
                }
            }
        }
    }
}
