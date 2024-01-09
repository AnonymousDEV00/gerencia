using System.Threading.Tasks;
using GerenciaVendas.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using GerenciaVendas.Models;
using GerenciaVendas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaVendas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly HashService _hashService;

        public UsuarioController(UsuarioService usuarioService, HashService hashService)
        {
            _usuarioService = usuarioService;
            _hashService = hashService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Email, Senha, Estado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.DefinirSenha(usuario.Senha, _hashService);
                    await _usuarioService.AddUsuarioAsync(usuario);
                    TempData["SuccessMessage"] = "Usuário criado com sucesso.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log ex ou lidar com a exceção de outra forma
                    ModelState.AddModelError(string.Empty, "Erro ao criar usuário.");
                }
            }
            return View(usuario);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = new Usuario
                    {
                        Nome = model.Nome,
                        Email = model.Email,
                        Estado = true
                    };

                    newUser.DefinirSenha(model.Senha, _hashService);

                    await _usuarioService.AddUsuarioAsync(newUser);
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    // Log ex ou lidar com a exceção de outra forma
                    ModelState.AddModelError(string.Empty, "Erro ao registrar usuário.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioService.AuthenticateAsync(model.Email, model.Senha);
                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        // Outras informações relevantes do usuário como claims
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Page", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nome, Email, Senha, Estado")] Usuario usuarioAtualizado)
        {
            if (id != usuarioAtualizado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _usuarioService.GetUsuarioByIdAsync(id);
                    if (usuarioExistente != null)
                    {
                        usuarioExistente.Nome = usuarioAtualizado.Nome;
                        usuarioExistente.Email = usuarioAtualizado.Email;
                        usuarioExistente.Estado = usuarioAtualizado.Estado;

                        if (!string.IsNullOrWhiteSpace(usuarioAtualizado.Senha))
                        {
                            usuarioExistente.DefinirSenha(usuarioAtualizado.Senha, _hashService);
                        }

                        await _usuarioService.UpdateUsuarioAsync(usuarioExistente);
                        TempData["SuccessMessage"] = "Usuário atualizado com sucesso.";
                    }
                    else
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar usuário.");
                    // Log ex ou lidar com a exceção de outra forma
                }
            }
            return View(usuarioAtualizado);
        }

        public async Task<IActionResult> Ativar(int id)
        {
            try
            {
                await _usuarioService.AtivarUsuarioAsync(id);
                TempData["SuccessMessage"] = "Usuário ativado com sucesso.";
            }
            catch (Exception ex)
            {
                // Log ex ou lidar com a exceção de outra forma
                TempData["ErrorMessage"] = "Erro ao ativar usuário.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int id)
        {
            try
            {
                await _usuarioService.InativarUsuarioAsync(id);
                TempData["SuccessMessage"] = "Usuário inativado com sucesso.";
            }
            catch (Exception ex)
            {
                // Log ex ou lidar com a exceção de outra forma
                TempData["ErrorMessage"] = "Erro ao inativar usuário.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            // Este método retorna a view para alterar a senha.
            // Certifique-se de que apenas o usuário correto possa acessar esta página.
            return View(new ChangePasswordViewModel { UserId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(model.UserId);
                if (usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                    return View(model);
                }

                // Verificando se a senha atual está correta
                if (!_hashService.VerifyPasswordHash(model.SenhaAtual, usuario.SenhaHash))
                {
                    ModelState.AddModelError(string.Empty, "Senha atual incorreta.");
                    return View(model);
                }

                // Atualiza para a nova senha
                usuario.DefinirSenha(model.NovaSenha, _hashService);
                await _usuarioService.UpdateUsuarioAsync(usuario);

                TempData["SuccessMessage"] = "Senha alterada com sucesso.";
                return RedirectToAction("Details", new { id = model.UserId });
            }
            catch (Exception ex)
            {
                // Log the exception details for further investigation
                // Use a logging framework or system of your choice
                // Example: _logger.LogError(ex, "Error changing password for user {UserId}", model.UserId);

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao alterar a senha. Por favor, tente novamente.");
                return View(model);
            }
        }




        // Adicione outros métodos conforme necessário
    }
}