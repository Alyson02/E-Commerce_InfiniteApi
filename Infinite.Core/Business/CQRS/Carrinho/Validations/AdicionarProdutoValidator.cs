using FluentValidation;
using Infinite.Core.Business.CQRS.Carrinho.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Carrinho.Validations
{
    public class AdicionarProdutoValidator : AbstractValidator<AdicionaProdutoCommand>
    {
        public AdicionarProdutoValidator()
        {
            //RuleFor(x => x.Quantidade)
            //    .GreaterThanOrEqualTo(0)
            //    .WithMessage("Não é possivel adicionar ao carrinho o produto com quantidade 0");
        }
    }
}
