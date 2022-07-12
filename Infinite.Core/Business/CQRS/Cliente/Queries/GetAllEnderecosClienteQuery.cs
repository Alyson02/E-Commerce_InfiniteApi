

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

namespace Infinite.Core.Business.CQRS.Cliente.Queries
{
    public class GetAllEnderecosClienteQuery : IRequest<Response>
    {
        public int UserId { get; set; }
        public class GetAllEnderecosClienteQueryHandler : IRequestHandler<GetAllEnderecosClienteQuery, Response>
        {
            private readonly IServiceBase<EnderecoClienteEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public GetAllEnderecosClienteQueryHandler(IServiceBase<EnderecoClienteEntity> service, IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(GetAllEnderecosClienteQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _clienteService.CreateSpec(x => x.UsuarioId == query.UserId);
                    var cliente = await _clienteService.FindAsync(spec);

                    var specEndereco = _service.CreateSpec(x => x.ClienteId == cliente.ClienteId).AddInclude(x => x.Endereco); 
                    var enderecosCliente = await _service.ListAsync(specEndereco);

                    return new Response(enderecosCliente);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Enderecos do Cliente", e);
                }
            }
        }
    }
}
