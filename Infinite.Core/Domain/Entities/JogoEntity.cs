using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class JogoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JogoId { get; set; }

        public bool Status { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public int PontoPreco { get; set; }

        public int ProdutoId { get; set; }
        public virtual ProdutoEntity Produto { get; set; }

    }
}