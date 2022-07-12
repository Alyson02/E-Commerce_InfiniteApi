using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class ArquivoAzureModel
    {
        public string Nome { get; set; }
        public decimal Tamanho { get; set; }
        public string Url { get; set; }
        public string Tipo { get; set; }
    }
}
