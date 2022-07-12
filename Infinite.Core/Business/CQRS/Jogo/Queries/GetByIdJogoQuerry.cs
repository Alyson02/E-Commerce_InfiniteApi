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

namespace Infinite.Core.Business.CQRS.Jogo.Queries//colocar o namespace correto Ex.: .Produto.Commands
{
    public class GetByIdJogoQuerry : IRequest<Response>
    {
        //Props
        public int JogoId { get; set; }
        public class GetByIdJogoQuerryHandler : IRequestHandler<GetByIdJogoQuerry, Response>
        {
            private readonly IServiceBase<JogoEntity> _service;
            public GetByIdJogoQuerryHandler(IServiceBase<JogoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(GetByIdJogoQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.JogoId == command.JogoId);

                    var Jogo = await this._service.FindAsync(spec);

                    if (Jogo == null) throw new Exception("Jogo não encontrado");

                    return new Response(Jogo);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Jogo", e);
                }
            }
        }
    }
}
