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

namespace Infinite.Core.Business.CQRS.Cliente.Commands//colocar o namespace correto Ex.: .Produto.Commands
{
    public class UpdateClienteCommand : IRequest<Response>
    {
        //Props
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Response>
        {
            private readonly IServiceBase<ClienteEntity> _service;
            public UpdateClienteCommandHandler(IServiceBase<ClienteEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.ClienteId == command.ClienteId);

                    var cliente = await this._service.FindAsync(spec);

                    if (cliente == null) throw new Exception("Cliente não encontrado");

                    /*Exemplo*/
                    cliente.Nome = command.Nome;
                    cliente.Pontos = command.Pontos;
                    cliente.Tell = command.Tell;


                    await _service.UpdateAsync(cliente);

                    return new Response("Cliente Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Cliente", e);
                }
            }
        }
    }
}
