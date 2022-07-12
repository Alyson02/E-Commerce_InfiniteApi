using FluentValidation;
using Infinite.Core.Business.CQRS.Carrinho.Commands;
using Infinite.Core.Business.Services.Base;
using Infinite.Core.Business.Services.Carrinho;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Carrinho.Validations
{
    public class CreateCarrinhoValidator : AbstractValidator<CreateCarrinhoCommand>
    {
        public CreateCarrinhoValidator(ICarrinhoService service)
        {
            RuleFor(x => x)
                .Custom((carrinho, context) =>
                {
                    var spec = service.CreateSpec(x => x.Cliente.UsuarioId == carrinho.UserId && x.Status == false);
                    spec.AddInclude(x => x.Cliente);
                    var carrinhos = service.ListAsync(spec).Result;

                    if (carrinhos.Count() > 0)
                    {
                        context.AddFailure("Ultimo carrinho não foi fechado");
                    }
                });
        }
    }
}
