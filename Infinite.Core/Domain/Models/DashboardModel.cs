using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class DashboardModel
    {
        public int Visitantes { get; set; }
        public int Vendas { get; set; }
        public int NovasContas { get; set; }
        public int Pedidos { get; set; }
    }
}
