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

namespace Infinite.Core.Business.CQRS.Cliente.Commands
{

    public class DeleteClienteCommand : IRequest<Response>
    {
        //Props
        public int ClienteId { get; set; }

        public class UpdateClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Response>
        {
            private readonly IServiceBase<ClienteEntity> _service;
            public UpdateClienteCommandHandler(IServiceBase<ClienteEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(DeleteClienteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.ClienteId == command.ClienteId);

                    var Cliente = await this._service.FindAsync(spec);

                    if (Cliente == null) throw new Exception("Cliente não encontrado");

                    await _service.DeleteAsync(Cliente);

                    return new Response("Cliente Excluido com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a Eliminação dos dados do Cliente", e);
                }
            }
        }
    }
}