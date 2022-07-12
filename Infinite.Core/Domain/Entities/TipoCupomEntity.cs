using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class TipoCupomEntity
    {
        [Key]
        public int TipoCupomId { get; set; }
        public string Descricao { get; set; }
    }
}
