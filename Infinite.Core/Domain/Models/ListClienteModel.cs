using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    internal class ListClienteModel
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
    }
}
