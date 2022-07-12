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

namespace Infinite.Core.Business.CQRS.Agendamento.Queries//colocar o namespace correto Ex.: .Produto.Commands
{
    public class GetByIdAgendamentoQuerry : IRequest<Response>
    {
        //Props
        public int AgendamentoId { get; set; }
        public class GetByIdAgendamentoQuerryHandler : IRequestHandler<GetByIdAgendamentoQuerry, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            public GetByIdAgendamentoQuerryHandler(IServiceBase<AgendamentoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(GetByIdAgendamentoQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.AgendamentoId == command.AgendamentoId)
                        .AddInclude(x => x.Jogo)
                        .AddInclude(x => x.Cliente);

                    var Agendamento = await this._service.FindAsync(spec);

                    if (Agendamento == null) throw new Exception("Agendamento não encontrado");

                    return new Response(Agendamento);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Agendamento", e);
                }
            }
        }
    }
}
