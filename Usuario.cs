using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciaVendas.Services;

namespace GerenciaVendas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter menos de 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string? Email { get; set; }

        // Hash da senha do usuário
        public string? SenhaHash { get; set; }

        [NotMapped] // Indica que esta propriedade não deve ser mapeada para o banco de dados
        public string? Senha { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Estado { get; set; } // Ativo/Inativo

        // Método para definir a senha
        public void DefinirSenha(string senha, HashService hashService)
        {
            SenhaHash = hashService.HashPassword(senha);
        }

        // Método para verificar a senha
        public bool VerificarSenha(string senha, HashService hashService)
        {
            if (string.IsNullOrEmpty(SenhaHash))
            {

                return false;
            }
            return hashService.VerifyPasswordHash(senha, SenhaHash);
        }
    }
}