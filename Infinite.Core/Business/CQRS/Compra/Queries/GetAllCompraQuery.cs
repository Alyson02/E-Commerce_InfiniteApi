

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

namespace Infinite.Core.Business.CQRS.Compra.Queries
{
    public class GetAllCompraQuery : IRequest<Response>
    {

        public class GetAllCompraQueryHandler : IRequestHandler<GetAllCompraQuery, Response>
        {
            private readonly IServiceBase<CompraEntity> _service;
            public GetAllCompraQueryHandler(IServiceBase<CompraEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllCompraQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var compras = await _service.ListAsync();

                    var model = compras.Select(x => new ListCompraModel
                    {
                        CarrinhoId = x.CarrinhoId,
                        CompraId = x.CompraId,
                        ValorFinal = x.Total
                    });

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar as compras", e);
                }
            }
        }
    }
}
