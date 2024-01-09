using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        // Se você incluir uma confirmação de senha, descomente as linhas abaixo
        /*
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        [Display(Name = "Confirme a Senha")]
        public string ConfirmacaoSenha { get; set; }
        */

        // Inclua outros campos conforme necessário, como 'Estado', se for relevante para o seu sistema
    }
}
