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
    public class UpdateFuncionarioCommand : IRequest<Response>
    {
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }    
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public class UpdateFuncionarioCommandHandler : IRequestHandler<UpdateFuncionarioCommand, Response>
        {
            private readonly IServiceBase<FuncionarioEntity> _service;
            public UpdateFuncionarioCommandHandler(IServiceBase<FuncionarioEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(UpdateFuncionarioCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.FuncionarioId == command.FuncionarioId);
                    spec.AddInclude(x => x.Usuario);

                    var Funcionario = await this._service.FindAsync(spec);

                    if (Funcionario == null) throw new Exception("Funcionario não encontrado");

                    /*Exemplo*/
                    Funcionario.Nome = command.Nome;
                    Funcionario.Telefone = command.Telefone;
                    Funcionario.Usuario.Email = command.Email;
                    Funcionario.Usuario.Password = command.Senha;

                    await _service.UpdateAsync(Funcionario);

                    return new Response("Funcionario Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Funcionario", e);
                }
            }
        }
    }
}
