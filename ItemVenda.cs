using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O identificador da venda é obrigatório")]
        public int IdVenda { get; set; } // Referência à venda associada

        [Required(ErrorMessage = "O identificador do produto é obrigatório")]
        public int IdProduto { get; set; } // Referência ao produto vendido

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço de venda não pode ser negativo")]
        public decimal PrecoVenda { get; set; }

        [Range(0, 100, ErrorMessage = "O desconto deve estar entre 0 e 100")]
        public decimal Desconto { get; set; } // Desconto aplicado ao item, em percentagem

        // Adicione aqui quaisquer outros métodos ou propriedades relevantes para o seu projeto
    }
}

