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
using Infinite.Core.Business.Services.FileUpload;

namespace Infinite.Core.Business.CQRS.Produto.Commands
{

    public class DeleteProdutoCommand : IRequest<Response>
    {
        public int ProdutoId { get; set; }

        public class UpdateProdutoCommandHandler : IRequestHandler<DeleteProdutoCommand, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            private readonly IServiceBase<ArquivoEntity> _arquivoService;
            public UpdateProdutoCommandHandler(IServiceBase<ProdutoEntity> service, IServiceBase<ArquivoEntity> arquivoService)
            {
                _service = service;
                _arquivoService = arquivoService;
            }

            public async Task<Response> Handle(DeleteProdutoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var spec = _service.CreateSpec(x => x.ProdutoID == command.ProdutoId);
                    spec.AddInclude(x => x.Capa)
                        .AddInclude(x => x.Categoria)
                        .AddInclude(x => x.Fotos)
                        .AddInclude("Fotos.Arquivo");

                    var produto = await _service.FindAsync(spec);

                    if (produto == null) throw new Exception("Produto não encontrado");

                    await _service.DeleteAsync(produto);

                    if (produto.Capa is not null)
                    {
                        await _arquivoService.DeleteAsync(produto.Capa);
                        await FileUploadService.DeleteFileAsync(produto.Capa.Nome, "infinite");
                    }

                    if(produto.Fotos is not null)
                    {
                        foreach (var foto in produto.Fotos)
                        {
                            await _arquivoService.DeleteAsync(foto.Arquivo);
                            await FileUploadService.DeleteFileAsync(foto.Arquivo.Nome, "infinite");
                        }
                    }

                    return new Response("Produto Excluido com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a Eliminação dos dados do Produto", e);
                }
            }
        }
    }
}