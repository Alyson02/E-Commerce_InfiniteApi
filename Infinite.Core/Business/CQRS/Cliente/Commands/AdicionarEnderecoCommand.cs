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
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infinite.Core.Business.CQRS.Cliente.Commands
{ 

    public class AdicionarEnderecoCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public EnderecoEntity Endereco { get; set; }
        public class AdicionarEnderecoCommandHandler : IRequestHandler<AdicionarEnderecoCommand, Response>
        {
            private readonly IServiceBase<EnderecoClienteEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public AdicionarEnderecoCommandHandler(IServiceBase<EnderecoClienteEntity> service, IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(AdicionarEnderecoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var spec = _clienteService.CreateSpec(x => x.UsuarioId == command.UserId);
                    var cliente = await _clienteService.FindAsync(spec);

                    var enderecoCliente = new EnderecoClienteEntity
                    {
                        ClienteId = cliente.ClienteId,
                        Endereco = command.Endereco
                    };

                    await _service.InsertAsync(enderecoCliente);

                    return new Response("Endereco adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Endereco", e);
                }
            }
        }
    }
}
