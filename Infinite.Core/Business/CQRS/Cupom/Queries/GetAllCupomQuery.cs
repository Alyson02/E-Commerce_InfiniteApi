using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Models;
using Infinite.Core.Infrastructure.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.CQRS.Cupom.Queries
{
    public class GetAllCupomQuery : IRequest<Response>
    {
        public int UsuarioId { get; set; }
        public class GetAllCupomQueryHandler : IRequestHandler<GetAllCupomQuery, Response>
        {
            private IServiceBase<CupomEntity> _service;

            public GetAllCupomQueryHandler(IServiceBase<CupomEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetAllCupomQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec();
                    var cupoms = await _service.ListAsync(cancellationToken);
                    return new Response(cupoms);
                }
                catch (Exception e)
                { 
                    throw new AppException("Erro ao buscar cupoms", e);
                }
            }
        }
    }
}
