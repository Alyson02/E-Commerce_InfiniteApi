using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    internal class ListJogoModel
    {
        public int JogoId { get; set; }
        public bool Status { get; set; }
        public string Nome { get; set; }
        public int PontoPreco { get; set; }
    }
}
