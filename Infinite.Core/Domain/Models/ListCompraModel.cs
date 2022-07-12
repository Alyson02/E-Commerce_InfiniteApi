using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class ListCompraModel
    {
        public int CompraId { get; set; }
        public int CarrinhoId { get; set; }
        public double ValorFinal { get; set; }
    }
}
