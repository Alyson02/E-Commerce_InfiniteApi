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
    public class GetCupomByIdQuery : IRequest<Response>
    {
        public string CupomId { get; set; }
        public class GetCupomByIdQueryHandler : IRequestHandler<GetCupomByIdQuery, Response>
        {
            private IServiceBase<CupomEntity> _service;

            public GetCupomByIdQueryHandler(IServiceBase<CupomEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetCupomByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CupomId == query.CupomId);
                    var cupom = await _service.FindAsync(spec);

                    if (cupom == null) throw new Exception("Cupom não encontrado");

                    return new Response(cupom);
                }
                catch (Exception e)
                { 
                    throw new AppException("Erro ao buscar cupoms", e);
                }
            }
        }
    }
}
