using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class UsuarioEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TipoUsuarioId { get; set; }
        public TipoUsuarioEntity TipoUsuario { get; set; }
    }
}
