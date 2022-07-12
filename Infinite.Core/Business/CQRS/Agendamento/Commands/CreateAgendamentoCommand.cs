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
{ //colocar o namespace correto Ex.: .Produto.Commands

    public class CreateAgendamentoCommand : IRequest<Response>
    {
        //Props
        public int UsuarioId { get; set; }
        public DateTime Horario { get; set; }
        public int Pontos { get; set; }

        // Chave entrangeira
        public int JogoId { get; set; }
        public int MaquinaId { get; set; }
        
        public class CreateAgendamentoCommandHandler : IRequestHandler<CreateAgendamentoCommand, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            private readonly IServiceBase<ClienteEntity> _clientService;
            public CreateAgendamentoCommandHandler(IServiceBase<AgendamentoEntity> service, IServiceBase<ClienteEntity> serviceCliente)
            {
                _service = service;
                _clientService = serviceCliente;
            }

            public async Task<Response> Handle(CreateAgendamentoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var spec = this._clientService.CreateSpec(x => x.UsuarioId == command.UsuarioId)
                        .AddInclude(x => x.Usuario);

                    var cliente = await this._clientService.FindAsync(spec);

                    var specAgenda = _service.CreateSpec(x => x.ClienteId == cliente.ClienteId && x.Status == false);
                    var agenda = await _service.FindAsync(specAgenda);

                    if (agenda != null) throw new Exception("Você ainda tem uma agenda em aberto");

                    var Agendamento = new AgendamentoEntity
                    {
                        Horario = command.Horario,
                        Pontos = command.Pontos, //Validar com o Alyson
                        ClienteId = cliente.ClienteId,
                        JogoId = command.JogoId,
                        MaquinaId = command.MaquinaId,
                    };

                    await _service.InsertAsync(Agendamento);

                    return new Response("Agendamento adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Agendamento", e);
                }
            }
        }
    }
}
