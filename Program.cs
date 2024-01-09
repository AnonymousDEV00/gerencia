using GerenciaVendas.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Adiciona e configura serviços de MVC
builder.Services.AddControllersWithViews();

// Configuração de autenticação baseada em Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login"; // Substitua pelo seu caminho de login, se diferente
                    options.AccessDeniedPath = "/Usuario/AccessDenied"; // Substitua pelo seu caminho de acesso negado, se necessário
                });

// Injeção de dependência para seus serviços
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<ItemVendaService>();
builder.Services.AddScoped<RelatorioService>();
builder.Services.AddScoped<HashService>();

// Adicione outras injeções de dependência conforme necessário

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Page}/{id?}");
});

app.Run();
