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
using Infinite.Core.Infrastructure.Repository;

namespace Infinite.Core.Business.CQRS.Dashboard.Queries
{
    public class GetTop10Query : IRequest<Response>
    {

        public class GetTop10QueryHandler : IRequestHandler<GetTop10Query, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetTop10QueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(GetTop10Query query, CancellationToken cancellationToken)
            {
                try
                {
                    var sqlTop10 = @"select COUNT(ic.ProdutoID) as ""NumeroDevendas"", p.Nome
                                        from itemcarrinho ic inner
                                        join
                                        produto p on p.ProdutoID = ic.ProdutoID GROUP BY p.Nome
                                        limit 10;";
                    var Top10 = await _unitOfWork.QuerySQLAsync<DashboardTop10Model>(sqlTop10, cancellationToken);

                    return new Response(Top10);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Dashboard", e);
                }
            }
        }
    }
}
