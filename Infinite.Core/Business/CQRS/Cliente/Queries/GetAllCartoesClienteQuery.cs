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
    public class GetAllCartoesClienteQuery : IRequest<Response>
    {
        public int UserId { get; set; }
        public class GetAllCartoesClienteQueryHandler : IRequestHandler<GetAllCartoesClienteQuery, Response>
        {
            private readonly IServiceBase<CartaoClienteEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public GetAllCartoesClienteQueryHandler(IServiceBase<CartaoClienteEntity> service, IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(GetAllCartoesClienteQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _clienteService.CreateSpec(x => x.UsuarioId == query.UserId);
                    var cliente = await _clienteService.FindAsync(spec);

                    var specCartao = _service.CreateSpec(x => x.ClienteId == cliente.ClienteId).AddInclude(x => x.Cartao); 
                    var cartoesCliente = await _service.ListAsync(specCartao);

                    return new Response(cartoesCliente);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Cartoes do Cliente", e);
                }
            }
        }
    }
}
