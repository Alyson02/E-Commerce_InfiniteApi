using Infinite.Core.Business.Services.Base;
using Infinite.Core.Business.Services.Carrinho;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Models;
using Infinite.Core.Infrastructure.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Carrinho.Commands
{
    public class FecharCarrinhoCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public int CarrinhoId { get; set; }
        public int EnderecoId { get; set; }
        public int FormaPagamentoId { get; set; }
        public int CartaoId { get; set; }
        public string CupomId { get; set; }
        public class FecharCarrinhoCommandHandler : IRequestHandler<FecharCarrinhoCommand, Response>
        {
            private readonly ICarrinhoService _service;
            private readonly IServiceBase<CompraEntity> _compraService;
            private readonly IServiceBase<CupomEntity> _cupomService;

            public FecharCarrinhoCommandHandler(ICarrinhoService service, 
                                                IServiceBase<CompraEntity> compraService,
                                                IServiceBase<CupomEntity> cupomService)
            {
                _service = service;
                _compraService = compraService;
                _cupomService = cupomService;
            }

            public async Task<Response> Handle(FecharCarrinhoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CarrinhoId == command.CarrinhoId);
                    spec.AddInclude(x => x.Produtos)
                        .AddInclude("Produtos.Produto")
                        .AddInclude(x => x.Cliente);
                    var carrinho = await _service.FindAsync(spec);

                    if (carrinho == null) throw new Exception("Carrinho não encontrado, verifique o id");

                    carrinho.Status = true;
                    await _service.UpdateAsync(carrinho);

                    var compra = new CompraEntity { CarrinhoId = carrinho.CarrinhoId};

                    foreach (var item in carrinho.Produtos)
                    {
                        compra.Total += item.ValorUnidade * item.Quantidade;
                        compra.Pontos += item.Pontos * item.Quantidade;
                    }

                    compra.Pagamento = new PagamentoEntity { FormaId = command.FormaPagamentoId };
                    if (command.FormaPagamentoId == (int)TipoPagamentoEnum.CARTAO)
                    {
                        compra.CartaoId = command.CartaoId;
                    }
                    else
                    {
                        compra.CartaoId = null;
                    }
                    
                    compra.EnderecoId = command.EnderecoId;

                    if(!string.IsNullOrEmpty(command.CupomId))
                    {
                        var card = await _cupomService.FindAsync(_cupomService.CreateSpec(x => x.CupomId == command.CupomId));
                        card.VendasRealizadas++;

                        compra.CupomId = card.CupomId;

                        if (card.TipoCupomId == (int)TipoCupomEnum.promocional) compra.Desconto = compra.Total * 0.15;
                        else if (card.TipoCupomId == (int)TipoCupomEnum.marketing) compra.Desconto = compra.Total * 0.10;
                        else compra.Desconto = compra.Total * 0.05;

                        compra.Total -= compra.Desconto;
                    }
                    else
                    {
                        compra.Cupom = null;
                    }

                    await _compraService.InsertAsync(compra);

                    return new Response($"Carrinho fechado com sucesso, foi gerado uma nova venda com o Id {compra.CompraId}");
                }
                catch (Exception e)
                {

                    throw new AppException("Erro ao fechar carrinho", e);
                }
            }
        }
    }
}
