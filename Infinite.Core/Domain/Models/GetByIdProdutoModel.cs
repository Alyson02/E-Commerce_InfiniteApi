using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class GetByIdProdutoModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public double Preco { get; set; }
        public string UrlCapa { get; set; }
        public string Categoria { get; set; }
        public int CategoriaId { get; set; }
        public List<string> UrlFotos { get; set; }
    }
}
