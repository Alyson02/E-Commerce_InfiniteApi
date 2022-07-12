using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class EnderecoClienteEntity
    {
        [Key()]
        public int ClienteId { get; set; }
        [Key()]
        public int EnderecoId { get; set; }
        public EnderecoEntity Endereco { get; set; }
    }
}
