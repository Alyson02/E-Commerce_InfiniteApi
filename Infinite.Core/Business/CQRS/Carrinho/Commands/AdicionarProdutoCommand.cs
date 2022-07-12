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

    public class AdicionaProdutoCommand : IRequest<Response>
    {
        public int ProdutoId { get; set; }
        public int UserId { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnidade { get; set; }
        public class AdicionaProdutoCommandHandler : IRequestHandler<AdicionaProdutoCommand, Response>
        {
            private readonly IServiceBase<ItemCarrinhoEntity> _service;
            private readonly IServiceBase<CarrinhoEntity> _carrinhoService;
            public AdicionaProdutoCommandHandler(IServiceBase<ItemCarrinhoEntity> service, 
                                                 IServiceBase<CarrinhoEntity> carrinhoService)
            {
                _service = service;
                _carrinhoService = carrinhoService;
            }

            public async Task<Response> Handle(AdicionaProdutoCommand command, CancellationToken cancellationToken)
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
                        if (lastCarrinho.Produtos.Any(x => x.ProdutoID == command.ProdutoId))
                        {
                            var produto = await _service.FindAsync(_service.CreateSpec(x => x.ProdutoID == command.ProdutoId && x.CarrinhoID == lastCarrinho.CarrinhoId));
                            produto.Quantidade += command.Quantidade;

                            await _service.UpdateAsync(produto);
                        }
                        else
                        {
                            var itemCarrinho = new ItemCarrinhoEntity
                            {
                                ProdutoID = command.ProdutoId,
                                CarrinhoID = lastCarrinho.CarrinhoId,
                                Quantidade = command.Quantidade,
                                ValorUnidade = command.ValorUnidade
                            };

                            await _service.InsertAsync(itemCarrinho);
                        }
                        return new Response("Produto adicionado com sucesso");
                    }

                    throw new Exception("Abra um carrinho para adicionar um produto");
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
