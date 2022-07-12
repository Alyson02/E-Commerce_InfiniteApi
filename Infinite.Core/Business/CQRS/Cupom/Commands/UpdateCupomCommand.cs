using FluentValidation;
using Infinite.Core.Business.Services.Base;
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

namespace Infinite.Core.Business.CQRS.Cupom.Commands
{
    public class UpdateCupomCommand : IRequest<Response>
    {
        public string CupomId { get; set; }
        public int TipoCupomId { get; set; }

        public class UpdateCupomCommandHandler : IRequestHandler<UpdateCupomCommand, Response>
        {
            private readonly IServiceBase<CupomEntity> _service;

            public UpdateCupomCommandHandler(IServiceBase<CupomEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(UpdateCupomCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CupomId == command.CupomId);
                    var cupom = await _service.FindAsync(spec, cancellationToken);

                    if (cupom == null) throw new Exception("Cupom não encontrado");

                    cupom.TipoCupomId = command.TipoCupomId;
                    await _service.UpdateAsync(cupom);

                    return new Response("Cupom atualizado com sucesso");
                }
                catch (ValidationException ve)
                {
                    throw new ValidationException(ve.Message);
                }
                catch (Exception e)
                {

                    throw new AppException("Erro ao atualizar cupom", e);
                }
            }
        }
    }
}
