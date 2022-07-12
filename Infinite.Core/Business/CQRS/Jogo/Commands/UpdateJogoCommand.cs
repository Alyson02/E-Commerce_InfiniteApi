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

namespace Infinite.Core.Business.CQRS.Jogo.Commands//colocar o namespace correto Ex.: .Produto.Commands
{
    public class UpdateJogoCommand : IRequest<Response>
    {
        //Props
        public int JogoId { get; set; }
        public bool Status { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public int PontoPreco { get; set; }


        public class UpdateJogoCommandHandler : IRequestHandler<UpdateJogoCommand, Response>
        {
            private readonly IServiceBase<JogoEntity> _service;
            public UpdateJogoCommandHandler(IServiceBase<JogoEntity> service)
            {
                this._service = service;
            }

            public async Task<Response> Handle(UpdateJogoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = this._service.CreateSpec(x => x.JogoId == command.JogoId);

                    var Jogo = await this._service.FindAsync(spec);

                    if (Jogo == null) throw new Exception("Jogo não encontrado");

                    /*Exemplo*/
                    Jogo.Status = command.Status;
                    Jogo.Descrição = command.Descrição;
                    Jogo.PontoPreco = command.PontoPreco;
                    Jogo.Nome = command.Nome;


                    await _service.UpdateAsync(Jogo);

                    return new Response("Jogo Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Jogo", e);
                }
            }
        }
    }
}
