using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciaVendas.ViewModels
{
    public class RelatorioViewModel
    {
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Início")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Fim")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Usuário/Vendedor")]
        public int? UsuarioVendedorId { get; set; }

        // Outros campos necessários para o relatório
        // Exemplo: Tipo de relatório, categorias específicas, etc.

        // Construtor para inicializar com valores padrão, se necessário
        public RelatorioViewModel()
        {
            DataInicio = DateTime.Now.AddDays(-30); // Exemplo: Últimos 30 dias
            DataFim = DateTime.Now;
        }
    }
}
