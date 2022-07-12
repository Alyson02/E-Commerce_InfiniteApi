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

namespace Infinite.Core.Business.CQRS.Categoria.Commands
{
    public class CreateCategoriaCommand : IRequest<Response>
    {
        public string Categoria { get; set; }
        public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, Response>
        {
            private readonly IServiceBase<CategoriaEntity> _service;
            public CreateCategoriaCommandHandler(IServiceBase<CategoriaEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(CreateCategoriaCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var categoria = new CategoriaEntity
                    {
                        Categoria = command.Categoria,
                    };

                    await _service.InsertAsync(categoria);

                    return new Response("Categoria adicionada com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer alguma coisa", e);
                }
            }
        }
    }
}

