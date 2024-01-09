namespace GerenciaVendas.ViewModels
{
    public class UsuarioEditModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public bool Estado { get; set; } // Ativo/Inativo

        // Outros campos conforme necessário
    }

}
