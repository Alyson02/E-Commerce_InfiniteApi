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

namespace Infinite.Core.Business.CQRS.Maquina.Commands
{ //colocar o namespace correto Ex.: .Produto.Commands

    public class CreateMaquinaCommand : IRequest<Response>
    {
        //Props
        public bool Status { get; set; }

        public class CreateMaquinaCommandHandler : IRequestHandler<CreateMaquinaCommand, Response>
        {
            private readonly IServiceBase<MaquinaEntity> _service;
            public CreateMaquinaCommandHandler(IServiceBase<MaquinaEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateMaquinaCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var Maquina = new MaquinaEntity
                    {
                        Status = command.Status
                    };

                    await _service.InsertAsync(Maquina);

                    return new Response("Maquina adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Maquina", e);
                }
            }
        }
    }
}
