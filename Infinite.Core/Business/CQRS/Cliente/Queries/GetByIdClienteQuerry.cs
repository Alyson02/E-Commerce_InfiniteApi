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

namespace Infinite.Core.Business.CQRS.Cliente.Queries//colocar o namespace correto Ex.: .Produto.Commands
{
    public class GetByIdClienteQuerry : IRequest<Response>
    {
        //Props
        public int ClienteId { get; set; }
        public class GetByIdClienteQuerryHandler : IRequestHandler<GetByIdClienteQuerry, Response>
        {
            private readonly IServiceBase<ClienteEntity> _service;
            public GetByIdClienteQuerryHandler(IServiceBase<ClienteEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(GetByIdClienteQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.ClienteId == command.ClienteId);
                    spec.AddInclude(x => x.Usuario);

                    var cliente = await this._service.FindAsync(spec);

                    if (cliente == null) throw new Exception("Cliente não encontrado");

                    return new Response(cliente);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Cliente", e);
                }
            }
        }
    }
}
