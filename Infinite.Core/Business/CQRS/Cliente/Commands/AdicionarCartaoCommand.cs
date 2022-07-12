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

    public class AdicionarCartaoCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public CartaoEntity Cartao { get; set; }
        public class AdicionarCartaoCommandHandler : IRequestHandler<AdicionarCartaoCommand, Response>
        {
            private readonly IServiceBase<CartaoClienteEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public AdicionarCartaoCommandHandler(IServiceBase<CartaoClienteEntity> service, IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(AdicionarCartaoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var spec = _clienteService.CreateSpec(x => x.UsuarioId == command.UserId);
                    var cliente = await _clienteService.FindAsync(spec);

                    var CartaoCliente = new CartaoClienteEntity
                    {
                        ClienteId = cliente.ClienteId,
                        Cartao = command.Cartao
                    };

                    await _service.InsertAsync(CartaoCliente);

                    return new Response("Cartao adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Cartao", e);
                }
            }
        }
    }
}
