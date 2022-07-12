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

namespace Infinite.Core.Business.CQRS.Jogo.Commands
{

    public class DeleteJogoCommand : IRequest<Response>
    {
        //Props
        public int JogoId { get; set; }

        public class UpdateJogoCommandHandler : IRequestHandler<DeleteJogoCommand, Response>
        {
            private readonly IServiceBase<JogoEntity> _service;
            public UpdateJogoCommandHandler(IServiceBase<JogoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(DeleteJogoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.JogoId == command.JogoId);

                    var Jogo = await this._service.FindAsync(spec);

                    if (Jogo == null) throw new Exception("Jogo não encontrado");

                    await _service.DeleteAsync(Jogo);

                    return new Response("Jogo Excluido com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a Eliminação dos dados do Jogo", e);
                }
            }
        }
    }
}