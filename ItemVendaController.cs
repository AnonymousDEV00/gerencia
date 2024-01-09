using Microsoft.AspNetCore.Mvc;
using GerenciaVendas.Models;
using GerenciaVendas.Services;
using System.Threading.Tasks;

namespace GerenciaVendas.Controllers
{
    public class ItemVendaController : Controller
    {
        private readonly ItemVendaService _itemVendaService;

        public ItemVendaController(ItemVendaService itemVendaService)
        {
            _itemVendaService = itemVendaService;
        }

        public async Task<IActionResult> Index()
        {
            var itensVenda = await _itemVendaService.GetAllItensVendaAsync();
            return View(itensVenda);
        }

        public IActionResult Create()
        {
            // Carregar informações necessárias, como lista de produtos e vendas
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                await _itemVendaService.AddItemVendaAsync(itemVenda);
                return RedirectToAction(nameof(Index));
            }
            // Recarregar informações necessárias em caso de falha
            return View(itemVenda);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var itemVenda = await _itemVendaService.GetItemVendaByIdAsync(id);
            if (itemVenda == null)
            {
                return NotFound();
            }
            // Carregar informações adicionais se necessário
            return View(itemVenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemVenda itemVenda)
        {
            if (id != itemVenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _itemVendaService.UpdateItemVendaAsync(itemVenda);
                return RedirectToAction(nameof(Index));
            }
            // Recarregar informações em caso de falha
            return View(itemVenda);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var itemVenda = await _itemVendaService.GetItemVendaByIdAsync(id);
            if (itemVenda == null)
            {
                return NotFound();
            }
            return View(itemVenda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _itemVendaService.DeleteItemVendaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
