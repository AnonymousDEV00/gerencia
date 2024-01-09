using Microsoft.AspNetCore.Mvc;
using GerenciaVendas.Models;
using GerenciaVendas.Services;
using System.Threading.Tasks;

namespace GerenciaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return View(produtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao", "Preco", "QuantidadeEstoque")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.AddProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Descricao", "Preco", "QuantidadeEstoque")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _produtoService.UpdateProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmed(int id)
        {
            await _produtoService.InativarProdutoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmed(int id)
        {
            await _produtoService.AtivarProdutoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Considere implementar um método Delete, se aplicável
        // ...
    }
}
