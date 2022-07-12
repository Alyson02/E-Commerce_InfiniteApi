using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class CartaoClienteEntity
    {
        [Key()]
        public int ClienteId { get; set; }
        [Key()]
        public int CartaoId { get; set; }
        public CartaoEntity Cartao { get; set; }
    }
}
