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

namespace Infinite.Core.Business.CQRS.Funcionario.Queries
{
    public class GetByIdFuncionarioQuerry : IRequest<Response>
    {
        public int FuncionarioId { get; set; }
        public class GetByIdFuncionarioQuerryHandler : IRequestHandler<GetByIdFuncionarioQuerry, Response>
        {
            private readonly IServiceBase<FuncionarioEntity> _service;
            public GetByIdFuncionarioQuerryHandler(IServiceBase<FuncionarioEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(GetByIdFuncionarioQuerry command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.FuncionarioId == command.FuncionarioId);
                    spec.AddInclude(x => x.Usuario)
                        .AddInclude(x => x.Usuario.TipoUsuario)
                        .AddInclude(x => x.Cupom)
                        .AddInclude(x => x.Cupom.TipoCupom);

                    var funcionario = await _service.FindAsync(spec);

                    if (funcionario == null) throw new Exception("Funcionario não encontrado");

                    return new Response(funcionario);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a consulta do Funcionario", e);
                }
            }
        }
    }
}
