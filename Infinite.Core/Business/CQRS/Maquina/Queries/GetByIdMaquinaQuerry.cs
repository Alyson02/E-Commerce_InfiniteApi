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

namespace Infinite.Core.Business.CQRS.Maquina.Queries//colocar o namespace correto Ex.: .Produto.Commands
{
    public class GetByIdMaquinaQuerry : IRequest<Response>
    {
        //Props
        public int MaquinaId { get; set; }
        public class GetByIdMaquinaQuerryHandler : IRequestHandler<GetByIdMaquinaQuerry, Response>
        {
            private readonly IServiceBase<MaquinaEntity> _service;
            public GetByIdMaquinaQuerryHandler(IServiceBase<MaquinaEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(GetByIdMaquinaQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.MaquinaId == command.MaquinaId);

                    var Maquina = await this._service.FindAsync(spec);

                    if (Maquina == null) throw new Exception("Maquina não encontrado");

                    return new Response(Maquina);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Maquina", e);
                }
            }
        }
    }
}
