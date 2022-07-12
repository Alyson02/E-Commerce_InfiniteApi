using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Models;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Infrastructure.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Agendamento.Commands
{

    public class DeleteAgendamentoCommand : IRequest<Response>
    {
        //Props
        public int AgendamentoId { get; set; }

        public class UpdateAgendamentoCommandHandler : IRequestHandler<DeleteAgendamentoCommand, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            public UpdateAgendamentoCommandHandler(IServiceBase<AgendamentoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(DeleteAgendamentoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.AgendamentoId == command.AgendamentoId);

                    var Agendamento = await this._service.FindAsync(spec);

                    if (Agendamento == null) throw new Exception("Agendamento não encontrado");

                    await _service.DeleteAsync(Agendamento);

                    return new Response("Agendamento Excluido com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a Eliminação dos dados do Agendamento", e);
                }
            }
        }
    }
}