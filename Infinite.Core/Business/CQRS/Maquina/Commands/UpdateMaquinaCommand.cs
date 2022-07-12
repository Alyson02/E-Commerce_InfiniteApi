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

namespace Infinite.Core.Business.CQRS.Maquina.Commands//colocar o namespace correto Ex.: .Produto.Commands
{
    public class UpdateMaquinaCommand : IRequest<Response>
    {
        //Props
        public int MaquinaId { get; set; }
        public bool Status { get; set; }

        public class UpdateMaquinaCommandHandler : IRequestHandler<UpdateMaquinaCommand, Response>
        {
            private readonly IServiceBase<MaquinaEntity> _service;
            public UpdateMaquinaCommandHandler(IServiceBase<MaquinaEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(UpdateMaquinaCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.MaquinaId == command.MaquinaId);

                    var Maquina = await this._service.FindAsync(spec);

                    if (Maquina == null) throw new Exception("Maquina não encontrado");

                    /*Exemplo*/
                    Maquina.Status = command.Status;

                    await _service.UpdateAsync(Maquina);

                    return new Response("Maquina Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Maquina", e);
                }
            }
        }
    }
}