

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
    public class GetCategoriasQuery : IRequest<Response>
    {

        public class GetCategoriasQueryHandler : IRequestHandler<GetCategoriasQuery, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetCategoriasQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(GetCategoriasQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var sqlCategorias = @"SELECT SUM(comp.Total) as NumeroVendas, cat.Categoria as Categoria
	                                        FROM itemcarrinho ic INNER JOIN
	                                        carrinho c ON c.CarrinhoId = ic.CarrinhoID INNER JOIN
	                                        compra comp ON comp.CarrinhoId = c.CarrinhoId INNER JOIN
	                                        produto p ON p.ProdutoID = ic.ProdutoID INNER JOIN 
	                                        categoria cat ON cat.categoriaId = p.categoriaId GROUP BY cat.Categoria";
                    var categorias = await _unitOfWork.QuerySQLAsync<DashboardCategoriaModel>(sqlCategorias, cancellationToken);

                    return new Response(categorias);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Dashboard", e);
                }
            }
        }
    }
}
