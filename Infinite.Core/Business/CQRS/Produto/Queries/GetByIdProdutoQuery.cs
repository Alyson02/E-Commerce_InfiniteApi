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

namespace Infinite.Core.Business.CQRS.Produto.Queries
{
    public class GetByIdProdutoQuerry : IRequest<Response>
    {
        public int ProdutoId { get; set; }
        public class GetByIdProdutoQuerryHandler : IRequestHandler<GetByIdProdutoQuerry, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            public GetByIdProdutoQuerryHandler(IServiceBase<ProdutoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetByIdProdutoQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.ProdutoID == command.ProdutoId);
                    spec.AddInclude(x => x.Capa)
                        .AddInclude(x => x.Categoria)
                        .AddInclude(x => x.Fotos)
                        .AddInclude("Fotos.Arquivo");

                    var produto = await _service.FindAsync(spec);

                    if (produto == null) throw new Exception("Produto não encontrado");

                    var produtoModel = new GetByIdProdutoModel
                    {
                        Titulo = produto.Nome,
                        Descricao = produto.Descricao,
                        Estoque = produto.Estoque,
                        Preco = produto.Preco,
                        UrlCapa = produto.Capa.Url,
                        Categoria = produto.Categoria.Categoria,
                        CategoriaId = produto.Categoria.CategoriaId,
                        UrlFotos = produto.Fotos.Select(x => x.Arquivo.Url).ToList()
                    };

                    return new Response(produtoModel);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Produto", e);
                }
            }
        }
    }
}
