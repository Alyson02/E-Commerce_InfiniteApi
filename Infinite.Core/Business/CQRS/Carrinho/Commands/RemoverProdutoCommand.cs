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
using FluentValidation;

namespace Infinite.Core.Business.CQRS.Carrinho.Commands
{

    public class RemoverProdutoCommand : IRequest<Response>
    {
        public int ProdutoId { get; set; }
        public int UserId { get; set; }
        public class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand, Response>
        {
            private readonly IServiceBase<ItemCarrinhoEntity> _service;
            private readonly IServiceBase<CarrinhoEntity> _carrinhoService;
            public RemoverProdutoCommandHandler(IServiceBase<ItemCarrinhoEntity> service,
                                                 IServiceBase<CarrinhoEntity> carrinhoService)
            {
                _service = service;
                _carrinhoService = carrinhoService;
            }

            public async Task<Response> Handle(RemoverProdutoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var spec = _carrinhoService.CreateSpec(x => x.Cliente.UsuarioId == command.UserId && x.Status == false);
                    spec.AddInclude(x => x.Cliente)
                        .AddInclude(x => x.Produtos);
                    var carrinhos = await _carrinhoService.ListAsync(spec);
                    var lastCarrinho = carrinhos.FirstOrDefault();

                    if (lastCarrinho != null)
                    {
                        var produto = await _service.FindAsync(_service.CreateSpec(x => x.ProdutoID == command.ProdutoId));

                        if(produto != null)
                        {
                            await _service.DeleteAsync(produto);
                            return new Response("Produto Removido com sucesso");
                        }

                        throw new Exception("Produto não está no carrinho");
                    }

                    throw new Exception("Abra um carrinho para remover um produto");
                }
                catch (ValidationException ve)
                {
                    throw new ValidationException(ve.Message);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Produto", e);
                }
            }
        }
    }
}
