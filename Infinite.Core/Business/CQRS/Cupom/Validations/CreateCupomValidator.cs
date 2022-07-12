using FluentValidation;
using Infinite.Core.Business.CQRS.Cupom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Cupom.Validations
{
    public class CreateCupomValidator : AbstractValidator<CreateCupomCommand>
    {
        public CreateCupomValidator()
        {
        }
    }
}
