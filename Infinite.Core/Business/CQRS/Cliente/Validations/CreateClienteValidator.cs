using FluentValidation;
using Infinite.Core.Business.CQRS.Cliente.Commands;
using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Cliente.Validations
{
    public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteValidator(IServiceBase<ClienteEntity> service)
        {
            RuleFor(x => x.Email)
                .Custom((email, contexto) =>
                {
                    var spec = service.CreateSpec(x => x.Usuario.Email == email)
                        .AddInclude(x => x.Usuario);
                    var temEmail = service.FindAsync(spec).Result;

                    if (temEmail != null)
                    {
                        contexto.AddFailure("Email já cadastrado");
                    }
                });

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Senha)
                .MinimumLength(8)
                .WithMessage("A senha tem que ter no minimo 8 caracteres")
                .MaximumLength(20)
                .WithMessage("A senha pode ter no maximo 20 caracteres")
                .NotEmpty()
                .WithMessage("A senha é obrigatória");
        }
    }
}
