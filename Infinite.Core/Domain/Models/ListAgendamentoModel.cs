using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    internal class ListAgendamentoModel
    {
        public int AgendamentoId { get; set; }
        public string Nome { get; set; }
        public DateTime Horario { get; set; }
        public string Jogo { get; set; }
    }
}
