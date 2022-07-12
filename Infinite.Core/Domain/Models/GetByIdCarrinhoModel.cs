using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class GetByIdCarrinhoModel : ListCarrinhoModel
    {
        public List<Produtos> Produtos { get; set; }
    }

    public class Produtos
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
    }
}
