using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class ArquivoEntity
    {
        [Key()]
        public int ArquivoId { get; set; }
        public string Nome { get; set; }
        public string Base64 { get; set; }
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Tamanho { get; set; }
        public string Url { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
