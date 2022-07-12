

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

namespace Infinite.Core.Business.CQRS.Carrinho.Queries
{
    public class GetAllCarrinhoQuery : IRequest<Response>
    {

        public class GetAllCarrinhoQueryHandler : IRequestHandler<GetAllCarrinhoQuery, Response>
        {
            private readonly IServiceBase<CarrinhoEntity> _service;
            public GetAllCarrinhoQueryHandler(IServiceBase<CarrinhoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllCarrinhoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec();
                    spec.AddInclude(x => x.Cliente)
                        .AddInclude(x => x.Produtos);
                    var carrinhos = await _service.ListAsync(spec);

                    var model = new List<ListCarrinhoModel>();

                    
                    foreach (var carrinho in carrinhos)
                    {
                        double total = 0;
                        foreach (var produto  in carrinho.Produtos)
                        {
                            total += produto.ValorUnidade * produto.Quantidade;
                        }
                        
                        model.Add(new ListCarrinhoModel
                        {
                            IdPedido = carrinho.CarrinhoId,
                            NomeCliente = carrinho.Cliente.Nome,
                            Status = carrinho.Status ? "Fechado" : "Aberto",
                            Valor = total
                        });
                    }

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Carrinho", e);
                }
            }
        }
    }
}
