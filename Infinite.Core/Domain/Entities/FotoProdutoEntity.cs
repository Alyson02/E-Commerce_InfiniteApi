using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class FotoProdutoEntity
    {
        [Key]
        public int ProdutoId { get; set; }
        [Key]
        public int ArquivoId { get; set; }
        public ArquivoEntity Arquivo { get; set; }
    }
}
