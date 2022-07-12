using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Models;
using Infinite.Core.Infrastructure.Helper;
using MediatR;

namespace Infinite.Core.Business.CQRS.Cupom.Commands
{
    public class CreateCupomCommand : IRequest<Response>
    {
        public int TipoCupomId { get; set; }

        public class CreateCupomCommandHandler : IRequestHandler<CreateCupomCommand, Response>
        {
            private readonly IServiceBase<CupomEntity> _service;

            public CreateCupomCommandHandler(IServiceBase<CupomEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateCupomCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var cupom = new CupomEntity
                    {
                        CupomId = Guid.NewGuid().ToString().Substring(0, 8),
                        TipoCupomId = command.TipoCupomId,
                    };

                    await _service.InsertAsync(cupom);

                    return new Response("Cupom criado com sucesso");
                }
                catch (ValidationException ve)
                {
                    throw new ValidationException(ve.Message);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao criar cupom", e);
                }
            }
        }
    }
}
