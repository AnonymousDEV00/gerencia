using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição deve ter menos de 200 caracteres.")]
        public string Descricao { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa")]
        public int QuantidadeEstoque { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Estado { get; set; } // Ativo/Inativo

        // Adicione aqui quaisquer outros métodos ou propriedades relevantes para o seu projeto
    }
}