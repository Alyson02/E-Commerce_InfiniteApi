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

namespace Infinite.Core.Business.CQRS.Categoria.Queries
{
    public class GetlAllCategoriaQuery : IRequest<Response>
    {
        //Props
        public class GetlAllCategoriaQueryHandler : IRequestHandler<GetlAllCategoriaQuery, Response>
        {
            private readonly IServiceBase<CategoriaEntity> _service;
            public GetlAllCategoriaQueryHandler(IServiceBase<CategoriaEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetlAllCategoriaQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var categorias = await _service.ListAsync();
                    return new Response(categorias);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer alguma coisa", e);
                }
            }
        }
    }
}
