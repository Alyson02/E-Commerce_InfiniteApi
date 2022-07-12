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
    public class CreateProdutoCommand : IRequest<Response>
    {
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public double Preco { get; set; }
        public int Pontos { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; }
        public ArquivoEntity Capa { get; set; }
        public List<ArquivoEntity> Fotos { get; set; }
        public class CreateProdutoHandler : IRequestHandler<CreateProdutoCommand, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            public CreateProdutoHandler(IServiceBase<ProdutoEntity> service)
            {
                _service = service; 
            }

            public async Task<Response> Handle(CreateProdutoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var uploadCapa = await FileUploadService.UploadFileAsync(command.Capa.Base64, "infinite", command.Capa.Tipo);

                    var produto = new ProdutoEntity
                    {
                        Nome = command.Nome,
                        Estoque = command.Estoque,
                        Preco = command.Preco,
                        Pontos = command.Pontos,
                        CategoriaId = command.CategoriaId,
                        Descricao = command.Descricao,
                        Capa = new ArquivoEntity
                        {
                            Base64 = "",
                            Nome = uploadCapa.Nome,
                            Url = uploadCapa.Url,
                            Tamanho = uploadCapa.Tamanho,
                            Tipo = uploadCapa.Tipo,
                        },
                        Fotos = new List<FotoProdutoEntity>()
                    };

                    if(command.Fotos.Count > 0)
                    {
                        foreach (var foto in command.Fotos)
                        {
                            var upload = await FileUploadService.UploadFileAsync(foto.Base64, "infinite", foto.Tipo);
                            produto.Fotos.Add(new FotoProdutoEntity
                            {
                                Arquivo = new ArquivoEntity
                                {
                                    Base64 = "",
                                    Url = upload.Url,
                                    Tipo = upload.Tipo,
                                    Tamanho = upload.Tamanho,
                                    Nome = upload.Nome,
                                }
                            });
                        }
                    }

                    await _service.InsertAsync(produto);

                    return new Response("Produto adicionado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao tentar adicionar novo produto", e);
                }
            }
        }
    }
}
