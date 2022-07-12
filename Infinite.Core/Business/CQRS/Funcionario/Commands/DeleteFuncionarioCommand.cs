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

namespace Infinite.Core.Business.CQRS.Funcionario.Commands
{

    public class DeleteFuncionarioCommand : IRequest<Response>
    {
        //Props
        public int FuncionarioId { get; set; }

        public class UpdateFuncionarioCommandHandler : IRequestHandler<DeleteFuncionarioCommand, Response>
        {
            private readonly IServiceBase<FuncionarioEntity> _service;
            public UpdateFuncionarioCommandHandler(IServiceBase<FuncionarioEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(DeleteFuncionarioCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.FuncionarioId == command.FuncionarioId);

                    var Funcionario = await this._service.FindAsync(spec);

                    if (Funcionario == null) throw new Exception("Funcionario não encontrado");

                    await _service.DeleteAsync(Funcionario);

                    return new Response("Funcionario Excluido com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a Eliminação dos dados do Funcionario", e);
                }
            }
        }
    }
}