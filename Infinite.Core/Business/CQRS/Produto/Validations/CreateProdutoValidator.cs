using FluentValidation;
using Infinite.Core.Business.CQRS.Produto.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Produto.Validations
{
    public class CreateProdutoValidator : AbstractValidator<CreateProdutoCommand>
    {
        public CreateProdutoValidator()
        {
            RuleFor(x => x.Fotos)
                .Must(list => list.Count <= 4 && list.Count > 2 )
                .WithMessage("Deve apenas 3 fotos");

            RuleFor(x => x.Capa)
                .NotEmpty()
                .WithMessage("Você deve adicionar pelo menos uma imagem para a capa");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Título é obrigatório!")
                .MinimumLength(10)
                .WithMessage("Título muito curto")
                .MaximumLength(40)
                .WithMessage("Título muito grande");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatório!")
                .MinimumLength(40)
                .WithMessage("Descrição muito curta")
                .MaximumLength(600)
                .WithMessage("Descrição muito grande");

            RuleFor(x => x.Preco)
                .NotEqual(0)
                .WithMessage("Vendendo de graça, tá sobrando dinheiro? (Preço deve ser maior que zero)");

            RuleFor(x => x.Estoque)
                .NotEqual(0)
                .WithMessage("Produto Fantasma detectado, estoque deve ser maior que 0");

            RuleFor(x => x.CategoriaId)
                .NotEqual(0)
                .WithMessage("Categoria é obrigatório");

        }
    }
}
