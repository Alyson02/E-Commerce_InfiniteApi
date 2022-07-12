

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
    public class GetContadoresQuery : IRequest<Response>
    {

        public class GetContadoresQueryHandler : IRequestHandler<GetContadoresQuery, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetContadoresQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(GetContadoresQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var sqlVisitante = @"SELECT COUNT(*) FROM visitantes";
                    var visitantes = await _unitOfWork.QueryFirstOrDefaultAsync<int>(sqlVisitante, cancellationToken);

                    var sqlVendas = @"SELECT COUNT(*) FROM compra";
                    var vendas = await _unitOfWork.QueryFirstOrDefaultAsync<int>(sqlVendas, cancellationToken);

                    var sqlContas = @"SELECT COUNT(*) FROM usuario";
                    var contas = await _unitOfWork.QueryFirstOrDefaultAsync<int>(sqlContas, cancellationToken);

                    var sqlCarrinho = @"SELECT COUNT(*) FROM carrinho";
                    var carrinhos = await _unitOfWork.QueryFirstOrDefaultAsync<int>(sqlCarrinho, cancellationToken);

                    var model = new DashboardModel
                    {
                        Visitantes = visitantes,
                        NovasContas = contas,
                        Vendas = vendas,
                        Pedidos = carrinhos,
                    };

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Dashboard", e);
                }
            }
        }
    }
}
