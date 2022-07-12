using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Filter;
using Infinite.Core.Domain.Models;
using Infinite.Core.Infrastructure.Helper;
using Infinite.Core.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Produto.Queries
{
    public class GetAllProdutoQuery : IRequest<Response>
    {
        public ProdutoFilter filter;
        public class GetAllProdutoQueryHandler : IRequestHandler<GetAllProdutoQuery, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            private readonly IServiceBase<VisitantesEntity> _visitantesService;

            public GetAllProdutoQueryHandler(IServiceBase<ProdutoEntity> service, IServiceBase<VisitantesEntity> visitantesService)
            {
                _service = service;
                _visitantesService = visitantesService; 
            }

            public async Task<Response> Handle(GetAllProdutoQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    await _visitantesService.InsertAsync(new VisitantesEntity());

                    var spec = new Specification<ProdutoEntity>();

                    if (!string.IsNullOrEmpty(query.filter.Nome))
                    {
                        spec = _service.CreateSpec(x => x.Nome.Contains(query.filter.Nome));
                    }
                    else
                    {
                        spec = _service.CreateSpec();
                    }
                    
                    spec.AddInclude(x => x.Categoria)
                        .AddInclude(x => x.Capa);
                    spec.ApplyOrderBy(x => x.Preco);

                    var produtos = await _service.ListAsync(spec);

                    return new Response(produtos.Select(produto => new ListProdutoModel
                    {
                        ProdutoID = produto.ProdutoID,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Categoria = produto.Categoria.Categoria,
                        UrlCapa = produto.Capa.Url
                    }));
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao listar produtos", e);
                }
            }
        }
    }
}
