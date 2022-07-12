using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class ProdutoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoID { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public double Preco { get; set; }
        public int Pontos { get; set; }
        public string Descricao { get; set; }
        // Chave entrangeira
        public int CategoriaId { get; set; }
        public virtual CategoriaEntity Categoria { get; set; }
        public int CapaId { get; set; }
        public ArquivoEntity Capa { get; set; }

        public ICollection<FotoProdutoEntity> Fotos { get; set; }
    }
}
