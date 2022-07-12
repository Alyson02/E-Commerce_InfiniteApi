using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class ListCarrinhoModel
    {
        public int IdPedido { get; set; }
        public string NomeCliente { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; }
    }
}
