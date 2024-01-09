using System;
using System.Collections.Generic;

namespace GerenciaVendas.ViewModels
{
    public class DetalhesVendaViewModel
    {
        public DateTime DataVenda { get; set; }
        public string NomeVendedor { get; set; }
        public decimal TotalVenda { get; set; }
        public List<ItemVendaDetalhe> ItensVenda { get; set; }

        public DetalhesVendaViewModel()
        {
            ItensVenda = new List<ItemVendaDetalhe>();
        }

        // Métodos adicionais, se necessário, para calcular totais ou formatar informações
    }
}
