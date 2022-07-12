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
    public class CreateFuncionarioCommand : IRequest<Response>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public class CreateFuncionarioCommandHandler : IRequestHandler<CreateFuncionarioCommand, Response>
        {
            private readonly IServiceBase<FuncionarioEntity> _service;
            public CreateFuncionarioCommandHandler(IServiceBase<FuncionarioEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateFuncionarioCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var funcionario = new FuncionarioEntity
                    {
                        Nome = command.Nome,
                        Telefone = command.Telefone,
                        Usuario = new UsuarioEntity
                        {
                            Email = command.Email,
                            Password = command.Senha,
                            TipoUsuarioId = (int)TipoUsuarioEnum.MASTER
                        },
                        Cupom = new CupomEntity
                        {
                            CupomId = Guid.NewGuid().ToString().Substring(0, 8),
                            TipoCupomId = (int)TipoCupomEnum.colaborador,
                        }
                    };

                    await _service.InsertAsync(funcionario);

                    return new Response("Funcionario adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Funcionario", e);
                }
            }
        }
    }
}
