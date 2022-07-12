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
    public class GetAllAgendamentoQuerry : IRequest<Response>
    {

        public class GetAllAgendamentoQuerryHandler : IRequestHandler<GetAllAgendamentoQuerry, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            public GetAllAgendamentoQuerryHandler(IServiceBase<AgendamentoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllAgendamentoQuerry request, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec()
                        .AddInclude(x => x.Jogo)
                        .AddInclude(x => x.Cliente);

                    spec.ApplyOrderBy(x => x.Horario);

                    var agendamento = await this._service.ListAsync(spec);

                    return new Response(
                        agendamento.Select(
                                x => new ListAgendamentoModel
                                {
                                    AgendamentoId = x.AgendamentoId,
                                    Nome = x.Cliente.Nome,
                                    Horario = x.Horario,
                                    Jogo = x.Jogo.Nome
                                }
                            ));
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Agendamento", e);
                }
            }
        }
    }
}
