

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

namespace Infinite.Core.Business.CQRS.Agendamento.Queries
{
    public class GetAgendaAtualQuery : IRequest<Response>
    {
        public int UserId { get; set; }
        public class GetAgendaAtualQueryHandler : IRequestHandler<GetAgendaAtualQuery, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public GetAgendaAtualQueryHandler(IServiceBase<AgendamentoEntity> service, IServiceBase<ClienteEntity> serviceCliente)
            {
                _service = service;
                _clienteService = serviceCliente;
            }

            public async Task<Response> Handle(GetAgendaAtualQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var specCliente = _clienteService.CreateSpec(x => x.UsuarioId == query.UserId);
                    var cliente = await _clienteService.FindAsync(specCliente);

                    var spec = _service.CreateSpec(x => x.ClienteId == cliente.ClienteId && x.Status == false);
                    spec.AddInclude(x => x.Jogo)
                        .AddInclude(x => x.Jogo.Produto)
                        .AddInclude(x => x.Jogo.Produto.Capa);

                    var agenda = await _service.FindAsync(spec);

                    if (agenda == null) throw new Exception("Nenhum agendamento encontrado, agenda um agora!");

                    if (agenda.Horario < DateTime.Now) 
                    {
                        agenda.Status = true;
                        agenda.Jogo.Produto.Capa = null;
                        agenda.Jogo.Produto = null;
                        agenda.Jogo = null;
                        await _service.UpdateAsync(agenda);
                        throw new Exception("Nenhuma agenda encontrada, cadestre uma nova");
                    } 

                    var model = new AgendamentoModel
                    {
                        DataAgendamento = agenda.Horario,
                        jogo = agenda.Jogo.Descrição,
                        ImageUrl = agenda.Jogo.Produto.Capa.Url
                    };

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Agenda", e);
                }
            }
        }
    }
}
