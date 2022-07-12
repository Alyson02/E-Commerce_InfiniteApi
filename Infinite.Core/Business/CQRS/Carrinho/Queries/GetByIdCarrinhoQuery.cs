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
    public class GetByIdCarrinhoQuerry : IRequest<Response>
    {
        public int CarrinhoId { get; set; }
        public class GetByIdCarrinhoQuerryHandler : IRequestHandler<GetByIdCarrinhoQuerry, Response>
        {
            private readonly IServiceBase<CarrinhoEntity> _service;
            public GetByIdCarrinhoQuerryHandler(IServiceBase<CarrinhoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetByIdCarrinhoQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CarrinhoId == command.CarrinhoId);
                    spec.AddInclude(x => x.Cliente)
                        .AddInclude(x => x.Produtos)
                        .AddInclude("Produtos.Produto");

                    var carrinho = await _service.FindAsync(spec);

                    if (carrinho == null) throw new Exception("Carrinho não encontrado");

                    double total = 0;

                    if(carrinho.Produtos.Count > 0)
                    {
                        foreach (var produto in carrinho.Produtos)
                        {
                            total += produto.ValorUnidade * produto.Quantidade;
                        }
                    }

                    var model = new GetByIdCarrinhoModel
                    {
                        IdPedido = carrinho.CarrinhoId,
                        NomeCliente = carrinho.Cliente.Nome,
                        Valor = total,
                        Status = carrinho.Status ? "Fechado" : "Aberto",
                        Produtos = carrinho.Produtos.Select(x => new Produtos
                        {
                            Nome = x.Produto.Nome,
                            Preco = x.Produto.Preco,
                            Quantidade = x.Quantidade
                        }).ToList()
                    };

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Carrinho", e);
                }
            }
        }
    }
}
