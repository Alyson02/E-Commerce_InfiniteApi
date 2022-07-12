using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class FormaPagEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FrmId { get; set; }
        [MaxLength(200)]
        public string Frm { get; set; }
    }
}
