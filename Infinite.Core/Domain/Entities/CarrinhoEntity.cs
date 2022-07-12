using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class CarrinhoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarrinhoId { get; set; }
        public bool Status { get; set; }
        public virtual DateTime DataCadastro { get;  set; } = DateTime.Now;
        public virtual DateTime? DataFechamento { get;  set; }

        // Chave entrangeira
        public int ClienteId { get; set; }
        public virtual ClienteEntity Cliente { get; set; }
        public virtual ICollection<ItemCarrinhoEntity> Produtos { get; set; }
    }
}