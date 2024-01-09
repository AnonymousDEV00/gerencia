using Microsoft.AspNetCore.Mvc;
using GerenciaVendas.Models;
using GerenciaVendas.Services;
using System.Threading.Tasks;

namespace GerenciaVendas.Controllers
{
    public class VendaController : Controller
    {
        private readonly VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public async Task<IActionResult> Index()
        {
            var vendas = await _vendaService.GetAllVendasAsync();
            return View(vendas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var venda = await _vendaService.GetVendaByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return View(venda);
        }

        public IActionResult Create()
        {
            // Aqui, considere carregar informações necessárias para a criação de uma venda, como lista de produtos e clientes
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario, TotalVenda")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                // Implementar lógica para preenchimento de detalhes da venda antes de salvar
                await _vendaService.AddVendaAsync(venda);
                return RedirectToAction(nameof(Index));
            }
            // Recarregar informações necessárias em caso de falha
            return View(venda);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var venda = await _vendaService.GetVendaByIdAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            // Carregar informações adicionais se necessário para edição
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, IdUsuario, TotalVenda")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Implementar verificação de alterações e salvar atualizações
                await _vendaService.UpdateVendaAsync(venda);
                return RedirectToAction(nameof(Index));
            }
            // Recarregar informações em caso de falha
            return View(venda);
        }

        // Considere adicionar métodos para gerenciar itens de venda, como adicionar ou remover produtos

        // Método Delete, se aplicável
        // ...
    }
}
