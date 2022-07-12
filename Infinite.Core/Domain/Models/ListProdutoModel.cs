using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class ListProdutoModel
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Categoria { get; set; }
        public string UrlCapa { get; set; }
    }
}
