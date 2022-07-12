using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Cupom.Commands
{
    public class DeleteCupomCommand : IRequest<Response>
    {
        public string CupomId { get; set; }

        public class DeleteCupomCommandHandler : IRequestHandler<DeleteCupomCommand, Response>
        {
            private readonly IServiceBase<CupomEntity> _service;

            public DeleteCupomCommandHandler(IServiceBase<CupomEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(DeleteCupomCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CupomId == command.CupomId);
                    var cupom = await _service.FindAsync(spec);

                    if (cupom == null) throw new Exception("Cupom não encontrado");

                    cupom.Status = false;

                    await _service.UpdateAsync(cupom);

                    return new Response("Cupom deletado com sucesso");
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao deletar cupom", e);
                }
            }
        }
    }
}
