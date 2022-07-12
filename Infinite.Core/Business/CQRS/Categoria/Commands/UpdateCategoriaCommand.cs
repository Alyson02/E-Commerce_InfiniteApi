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
    public class UpdateCategoriaCommand : IRequest<Response>
    {
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public class UpdateCategoriaCommandHandler : IRequestHandler<UpdateCategoriaCommand, Response>
        {
            private readonly IServiceBase<CategoriaEntity> _service;
            public UpdateCategoriaCommandHandler(IServiceBase<CategoriaEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(UpdateCategoriaCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.CategoriaId == command.CategoriaId);
                    var categoria = await _service.FindAsync(spec);
                    if (categoria == null) throw new Exception("Categoria não encontrada");

                    categoria.Categoria = command.Categoria;

                    await _service.UpdateAsync(categoria);

                    return new Response("Alguma coisa com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer alguma coisa", e);
                }
            }
        }
    }
}
