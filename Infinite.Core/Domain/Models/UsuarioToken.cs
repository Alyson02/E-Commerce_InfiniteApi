using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class UsuarioToken
    {
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
        public string Role { get; set; }
        public string Nome { get; set; }
    }
}
