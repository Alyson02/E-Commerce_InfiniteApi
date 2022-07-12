

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
    public class GetAllMaquinaQuerry : IRequest<Response>
    {

        public class GetAllMaquinaQuerryHandler : IRequestHandler<GetAllMaquinaQuerry, Response>
        {
            private readonly IServiceBase<MaquinaEntity> _service;
            public GetAllMaquinaQuerryHandler(IServiceBase<MaquinaEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllMaquinaQuerry request, CancellationToken cancellationToken)
            {
                try
                {
                    var Maquina = await this._service.ListAsync();

                    var model = Maquina.Select(x => new ListMaquinaModel
                    {
                        MaquinaId = x.MaquinaId,
                        Status = x.Status,
                    });

                    return new Response(model);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao Listar os Maquina", e);
                }
            }
        }
    }
}
