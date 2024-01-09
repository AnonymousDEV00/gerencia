using System;
using System.Collections.Generic;

namespace GerenciaVendas.ViewModels
{
    public class EditVendaViewModel
    {
        public int VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public List<ItemVendaEdit> ItensVenda { get; set; }

        public EditVendaViewModel()
        {
            ItensVenda = new List<ItemVendaEdit>();
        }

        // Métodos adicionais para manipulação dos itens de venda podem ser incluídos aqui, se necessário.
    }
}
