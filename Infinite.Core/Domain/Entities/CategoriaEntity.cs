using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class CategoriaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }

        [MaxLength(50)]
        public string Categoria { get; set; }

    }
}