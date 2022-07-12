using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace Infinite.Core.Domain.Entities
{
    public class FuncionarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuncionarioId { get; set; }
        [MaxLength(150)]
        public string Nome { get; set; }
        [MaxLength(14)]
        public string Telefone { get; set; }

        //chaves estrangeiras
        public string CupomId { get; set; }
        public virtual CupomEntity Cupom { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioEntity Usuario { get; set; }

    }
}
