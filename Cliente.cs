using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciaVendas.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter menos de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [Column(TypeName = "Date")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF/CNPJ deve ter entre 11 e 14 caracteres.")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço deve ter menos de 200 caracteres.")]
        public string Endereco { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Estado { get; set; } // Ativo/Inativo

        // Adicione aqui quaisquer outros métodos ou propriedades relevantes para o seu projeto
    }
}