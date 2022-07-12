using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
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
    public class GetTop4Query : IRequest<Response>
    {
        public class GetTop5QueryHandler : IRequestHandler<GetTop4Query, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            private readonly IUnitOfWork _unitOfWork;
            public GetTop5QueryHandler(IServiceBase<ProdutoEntity> service, IUnitOfWork unitOfWork)
            {
                _service = service;
                _unitOfWork = unitOfWork;   
            }

            public async Task<Response> Handle(GetTop4Query query, CancellationToken cancellationToken)
            {
                try
                {
                    var sqlTop5 = @"select t1.ProdutoID,
	                                        t1.Nome,
                                            t1.Preco,
                                            t3.Categoria,
                                            t2.Url as UrlCapa
                                    from produto t1 inner join 
                                    arquivo t2 on t2.ArquivoId = t1.CapaId inner join
                                    categoria t3 on t3.categoriaId = t1.categoriaId 
                                    where Descricao is not null limit 4";
                    var Top5 = await _unitOfWork.QuerySQLAsync<ListProdutoModel>(sqlTop5, cancellationToken);

                    return new Response(Top5);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao buscar top 5 produtos", e);
                }
            }
        }
    }
}
