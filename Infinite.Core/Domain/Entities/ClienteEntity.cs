using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class ClienteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(14)]
        public string Tell { get; set; }
        public int Pontos {get; set; }
        public int UsuarioId { get; set; }
        public UsuarioEntity Usuario { get; set; }

        public ICollection<CartaoClienteEntity> Cartoes { get; set; }
        public ICollection<EnderecoClienteEntity> Enderecos { get; set; }

    }
}
