using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class PagamentoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PagamentoId { get; set; }
        [MaxLength(100)]
        public string Dados { get; set; }

        //Chaves estrangeiras
        public int FormaId { get; set; }
        public virtual FormaPagEntity FormaPag { get; set; }
    }
}
