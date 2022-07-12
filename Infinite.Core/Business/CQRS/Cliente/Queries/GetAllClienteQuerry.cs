

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
    public class GetAllClienteQuerry : IRequest<Response>
    {
        
        public class GetAllClienteQuerryHandler : IRequestHandler<GetAllClienteQuerry, Response>
        {
            private readonly IServiceBase<ClienteEntity> _service;
            public GetAllClienteQuerryHandler(IServiceBase<ClienteEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllClienteQuerry request, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec().AddInclude(x => x.Usuario);
                    var Cliente = await this._service.ListAsync(spec);

                    var model = Cliente.Select(x => new ListClienteModel
                    {
                        ClienteId = x.ClienteId,
                        Nome = x.Nome,
                        Tell = x.Tell,
                        Email = x.Usuario.Email
                    });

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Cliente", e);
                }
            }
        }
    }
}
