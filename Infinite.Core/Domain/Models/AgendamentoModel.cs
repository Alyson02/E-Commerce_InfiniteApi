using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Models
{
    public class AgendamentoModel
    {
        public string jogo { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string ImageUrl { get; set; }
    }
}
