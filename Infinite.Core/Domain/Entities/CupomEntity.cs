using System.ComponentModel.DataAnnotations;

namespace Infinite.Core.Domain.Entities
{
    public class CupomEntity
    {
        [Key()]
        public string CupomId { get; set; }
        [MaxLength(20)]
        public int TipoCupomId { get; set; }
        public TipoCupomEntity TipoCupom { get; set; }
        public int VendasRealizadas { get; set; } = 0;
        public bool Status { get; set; } = true;
    }
}