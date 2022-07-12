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
    public class UpdateProdutoCommand : IRequest<Response>
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public double Preco { get; set; }
        public int Pontos { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; }
        public ArquivoEntity Capa { get; set; }
        public List<ArquivoEntity> Fotos { get; set; }

        public class UpdateProdutoCommandHandler : IRequestHandler<UpdateProdutoCommand, Response>
        {
            private readonly IServiceBase<ProdutoEntity> _service;
            public UpdateProdutoCommandHandler(IServiceBase<ProdutoEntity> service)
            {
                _service = service;
            }

            public async Task<Response> Handle(UpdateProdutoCommand command, CancellationToken cancellationToken)
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

                    if (command.Fotos.Count > 0)
                    {
                        var contador = 0;
                        foreach (var foto in command.Fotos)
                        {
                            var temFoto = contador < produto.Fotos.Count ? produto.Fotos.ToList()[contador] : null;
                            if (!string.IsNullOrEmpty(foto.Base64))
                            {
                                var upload = await FileUploadService.UploadFileAsync(foto.Base64, "infinite", foto.Tipo, temFoto == null ? "" : temFoto.Arquivo.Nome, temFoto != null);

                                if (temFoto != null)
                                {
                                    var arquivo = produto.Fotos.ToList()[contador].Arquivo;
                                    arquivo.Base64 = "";
                                    arquivo.Nome = upload.Nome;
                                    arquivo.Url = upload.Url;
                                    arquivo.Tamanho = upload.Tamanho;
                                    arquivo.Tipo = upload.Tipo;
                                }
                                else
                                {
                                    produto.Fotos.Add(new FotoProdutoEntity
                                    {
                                        Arquivo = new ArquivoEntity
                                        {
                                            Base64 = "",
                                            Nome = upload.Nome,
                                            Url = upload.Url,
                                            Tamanho = upload.Tamanho,
                                            Tipo = upload.Tipo,
                                        }
                                    });
                                }
                            }
                            contador++;
                        }
                    }

                    if (produto.Capa is not null && !string.IsNullOrEmpty(command.Capa.Base64))
                    {
                        var uploadCapa = await FileUploadService.UploadFileAsync(command.Capa.Base64, "infinite", command.Capa.Tipo, produto.Capa?.Nome, produto.Capa != null);

                        produto.Capa.Base64 = "";
                        produto.Capa.Nome = uploadCapa.Nome;
                        produto.Capa.Tamanho = uploadCapa.Tamanho;
                        produto.Capa.Tipo = uploadCapa.Tipo;
                        produto.Capa.Url = uploadCapa.Url;
                    }

                    produto.Preco = command.Preco;
                    produto.Descricao = command.Descricao;
                    produto.Pontos = command.Pontos;
                    produto.CategoriaId = command.CategoriaId;
                    produto.Nome = command.Nome;
                    produto.Estoque = command.Estoque;

                    await _service.UpdateAsync(produto);

                    return new Response("Produto Atualizado com sucesso");
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer a atualização dos dados do Produto", e);
                }
            }
        }
    }
}
