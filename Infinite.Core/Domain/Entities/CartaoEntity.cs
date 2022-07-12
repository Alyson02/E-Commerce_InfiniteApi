using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class CartaoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardId { get; set; }
        [MaxLength(16)]
        public string NumCard { get; set; }
        [MaxLength(3)]
        public string CodigoSeg { get; set; }
        [MaxLength (50)]
        public string Badeira { get; set; }
        [MaxLength (50)]
        public string ApelidoCard { get; set; }
    }
}
