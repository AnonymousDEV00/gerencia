using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        // Adicionando uma validação para a complexidade da nova senha
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "A senha deve conter pelo menos um dígito, uma letra maiúscula e uma letra minúscula.")]
        public string NovaSenha { get; set; }

        // Considerar adicionar uma propriedade para confirmar a nova senha
        // [Compare("NovaSenha", ErrorMessage = "As senhas não correspondem.")]
        // public string ConfirmarNovaSenha { get; set; }
    }
}

