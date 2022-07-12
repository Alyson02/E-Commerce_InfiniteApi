using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infinite.Core.Domain.Models;
using Infinite.Core.Infrastructure.Helper;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infinite.Core.Business.Services.FileUpload
{
    public static class FileUploadService
    {
        public static async Task<ArquivoAzureModel> UploadFileAsync(string fileBase64, string container, string tipoArquivo, string fileKey = "", bool update = false)
        {
            try
            {
                if (update)
                {
                    var blobClientDel = new BlobClient("DefaultEndpointsProtocol=https;AccountName=tccinfinite;AccountKey=Lvi6WGoS2O5KjdIxvGAbRfzxn5OLaJ9vyNxnKxy9C4gmYgfzoVkhHTsqgD8XTouXiz37kpUk2enF+ASt2KKiqw==;EndpointSuffix=core.windows.net", container, fileKey);
                    var delete = await blobClientDel.DeleteIfExistsAsync();
                }

                //Gera um nome randomico pra imagem
                var fileName = Guid.NewGuid().ToString() + MimeTypeMap.GetExtension(tipoArquivo);

                //limpa o has enviado
                var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(fileBase64, "");

                //gera um array de bytes
                byte[] fileBytes = Convert.FromBase64String(data);

                //Pega o tamanho do arquivo
                var fileSize = fileBytes.Length;

                // Define o BLOB no qual a imagem será armazenada
                var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=tccinfinite;AccountKey=Lvi6WGoS2O5KjdIxvGAbRfzxn5OLaJ9vyNxnKxy9C4gmYgfzoVkhHTsqgD8XTouXiz37kpUk2enF+ASt2KKiqw==;EndpointSuffix=core.windows.net", container, fileName);

                using (var ms = new MemoryStream(fileBytes))
                {
                    //Envia a imagem
                    await blobClient.UploadAsync(ms);
                }

                return new ArquivoAzureModel
                {
                    Url = blobClient.Uri.AbsoluteUri,
                    Nome = fileName,
                    Tamanho = fileSize,
                    Tipo = tipoArquivo
                };
            }
            catch (Exception e)
            {
                throw new AppException("Erro ao enviar aquivo", e);
            }
        }

        public static async Task<bool> DeleteFileAsync(string fileKey, string container)
        {
            if (!string.IsNullOrEmpty(fileKey))
            {
                var blobClientDel = new BlobClient("DefaultEndpointsProtocol=https;AccountName=tccinfinite;AccountKey=Lvi6WGoS2O5KjdIxvGAbRfzxn5OLaJ9vyNxnKxy9C4gmYgfzoVkhHTsqgD8XTouXiz37kpUk2enF+ASt2KKiqw==;EndpointSuffix=core.windows.net", container, fileKey);
                var del = await blobClientDel.DeleteIfExistsAsync();
                return del;
            }
            throw new Exception("Não foi possivel deletar arquivo por que nome está vazio");
        }
    }
}
