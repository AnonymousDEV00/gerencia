using Microsoft.AspNetCore.Mvc;
using GerenciaVendas.Models;
using GerenciaVendas.Services;
using System.Threading.Tasks;

namespace GerenciaVendas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,DataNascimento,CPF_CNPJ,Endereco")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.AddClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,CPF_CNPJ,Endereco,Estado")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _clienteService.UpdateClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Ativar(int id)
        {
            await _clienteService.AtivarClienteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int id)
        {
            await _clienteService.InativarClienteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Considere adicionar um método Delete, se for aplicável
        // ...
    }
}

