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

namespace Infinite.Core.Business.CQRS.Agendamento.Commands//colocar o namespace correto Ex.: .Produto.Commands
{
    public class UpdateAgendamentoCommand : IRequest<Response>
    {
        //Props
        public int AgendamentoId { get; set; }
        public DateTime Horario { get; set; }
        public int Pontos { get; set; }

        // Chave entrangeira
        public int ClienteId { get; set; }
        public int JogoId { get; set; }
        public int MaquinaId { get; set; }

        public class UpdateAgendamentoCommandHandler : IRequestHandler<UpdateAgendamentoCommand, Response>
        {
            private readonly IServiceBase<AgendamentoEntity> _service;
            public UpdateAgendamentoCommandHandler(IServiceBase<AgendamentoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(UpdateAgendamentoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.AgendamentoId == command.AgendamentoId);

                    var Agendamento = await this._service.FindAsync(spec);

                    if (Agendamento == null) throw new Exception("Agendamento não encontrado");


                    Agendamento.AgendamentoId = command.AgendamentoId;
                    Agendamento.Horario = command.Horario;
                    Agendamento.Pontos = command.Pontos;
                    Agendamento.ClienteId = command.ClienteId;
                    Agendamento.JogoId = command.JogoId;
                    Agendamento.MaquinaId = command.MaquinaId;

                    await _service.UpdateAsync(Agendamento);

                    return new Response("Agendamento Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Agendamento", e);
                }
            }
        }
    }
}
