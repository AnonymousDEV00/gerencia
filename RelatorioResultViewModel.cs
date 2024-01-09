using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GerenciaVendas.ViewModels
{
    public class RelatorioResultViewModel
    {
        public List<DadosRelatorioItem> DadosRelatorio { get; set; }

        public RelatorioResultViewModel()
        {
            DadosRelatorio = new List<DadosRelatorioItem>();
        }

        public decimal CalcularTotalVendas()
        {
            return DadosRelatorio.Sum(item => item.ValorTotal);
        }

        public string FormatarData(DateTime data)
        {
            return data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        // Aqui você pode adicionar mais métodos conforme necessário para o seu relatório
    }
}
