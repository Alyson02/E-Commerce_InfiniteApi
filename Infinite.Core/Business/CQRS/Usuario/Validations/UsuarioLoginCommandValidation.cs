using FluentValidation;
using Infinite.Core.Business.CQRS.Usuario.Commands;
using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Usuario.Validations
{
    public class UsuarioLoginCommandValidation : AbstractValidator<UsuarioLoginCommand>
    {
        public UsuarioLoginCommandValidation(IServiceBase<UsuarioEntity> service)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }

    }
}
