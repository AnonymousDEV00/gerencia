using System;
using System.Collections.Generic;

namespace GerenciaVendas.ViewModels
{
    public class NovaVendaViewModel
    {
        public DateTime DataVenda { get; set; }
        public List<ItemVendaInput> ItensVenda { get; set; }

        public NovaVendaViewModel()
        {
            ItensVenda = new List<ItemVendaInput>();
            DataVenda = DateTime.Now; // Configura a data de venda para a data atual por padrão.
        }

        // Métodos adicionais para manipulação dos itens de venda podem ser incluídos aqui.
    }
}
