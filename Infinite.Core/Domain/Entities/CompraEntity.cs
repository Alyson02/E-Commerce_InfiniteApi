using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infinite.Core.Domain.Entities
{
    public class CompraEntity
    {

        // Falta criar a tabela de cliente para adicionar o endereço e o codigo do funcionario
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompraId { get; set; }
        public double Total { get; set; }
        public double Desconto { get; set; }
        public double Pontos { get; set; }

        //chaves estrangeiras
        public string CupomId { get; set; }
        public virtual CupomEntity Cupom { get; set; }
        public int PagamentoId { get; set; }
        public virtual PagamentoEntity Pagamento { get; set; }
        public int EnderecoId { get; set; }
        public virtual EnderecoEntity Endereco { get; set; }
        public int? CartaoId { get; set; }
        public virtual CartaoEntity Cartao { get; set; }
        public int CarrinhoId { get; set; }
        public CarrinhoEntity Carrinho { get; set; }

    }
}
