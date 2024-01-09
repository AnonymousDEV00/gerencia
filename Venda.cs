using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.Models
{
    public class Venda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O identificador do usuário é obrigatório")]
        public int IdUsuario { get; set; } // Referência ao usuário que realizou a venda

        [Required(ErrorMessage = "A data da venda é obrigatória")]
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;

        [Range(0, double.MaxValue, ErrorMessage = "O total da venda não pode ser negativo")]
        public decimal TotalVenda { get; set; } // Pode ser calculado com base nos itens da venda

        public List<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();

        // Construtor sem parâmetros para Dapper
        public Venda() { }

        // Adicione aqui quaisquer outros métodos ou propriedades relevantes para o seu projeto
    }
}

