using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Domain.Entities
{
    public class VisitantesEntity
    {
        [Key]
        public int NumeroVisitas { get; set; }
    }
}
