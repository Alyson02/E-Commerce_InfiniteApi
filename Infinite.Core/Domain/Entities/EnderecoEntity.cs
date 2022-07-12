using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class EnderecoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EndId { get; set; }
        [MaxLength(9)]
        public string CEP { get; set; }
        [MaxLength(10)]
        public string NumCasa { get; set; }
        [MaxLength(50)]
        public string Apelido { get; set; }
        [MaxLength(20)]
        public string NomeRua { get; set; }
    }
}