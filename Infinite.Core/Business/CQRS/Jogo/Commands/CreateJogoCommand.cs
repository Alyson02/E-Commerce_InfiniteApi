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

namespace Infinite.Core.Business.CQRS.Jogo.Commands
{ //colocar o namespace correto Ex.: .Produto.Commands

    public class CreateJogoCommand : IRequest<Response>
    {
        //Props
        public bool Status { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public int PontoPreco { get; set; }

        public class CreateJogoCommandHandler : IRequestHandler<CreateJogoCommand, Response>
        {
            private readonly IServiceBase<JogoEntity> _service;
            public CreateJogoCommandHandler(IServiceBase<JogoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateJogoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var Jogo = new JogoEntity
                    {
                        Nome = command.Nome,
                        PontoPreco = command.PontoPreco,
                        Descrição = command.Descrição,
                        Status = command.Status
                    };

                    await _service.InsertAsync(Jogo);

                    return new Response("Jogo adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Jogo", e);
                }
            }
        }
    }
}
