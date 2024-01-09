using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GerenciaVendas.Services
{
    public class UsuarioService
    {
        private readonly string _connectionString;
        private readonly HashService _hashService;

        public UsuarioService(IConfiguration configuration, HashService hashService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("String de conexão 'DefaultConnection' não encontrada.");
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService), "HashService não pode ser nulo.");
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var query = "SELECT * FROM Usuarios_Dev WHERE Id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(query, new { Id = id });
                return usuario ?? throw new Exception("Usuário não encontrado."); // Trate o caso nulo
            }
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new InvalidOperationException("A senha não pode ser nula ou vazia.");
            }

            usuario.SenhaHash = _hashService.HashPassword(usuario.Senha);
            var query = "INSERT INTO Usuarios_Dev (Nome, Email, SenhaHash, DataCadastro, Estado) VALUES (@Nome, @Email, @SenhaHash, GETDATE(), @Estado)";
            await ExecuteQueryAsync(query, usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            var query = "SELECT * FROM Usuarios_Dev";
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Usuario>(query);
            }
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            // Considerar a lógica para atualizar a senha apenas se uma nova for fornecida
            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuario.SenhaHash = _hashService.HashPassword(usuario.Senha);
            }
            var query = "UPDATE Usuarios_Dev SET Nome = @Nome, Email = @Email, SenhaHash = @SenhaHash, Estado = @Estado WHERE Id = @Id";
            await ExecuteQueryAsync(query, usuario);
        }

        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            var usuario = await GetUsuarioByEmailAsync(email);
            if (usuario != null && !string.IsNullOrEmpty(usuario.SenhaHash) &&
                _hashService.VerifyPasswordHash(senha, usuario.SenhaHash))
            {
                return usuario; // Autenticação bem-sucedida
            }
            return null; // Falha na autenticação
        }



        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            var query = "SELECT * FROM Usuarios_Dev WHERE Email = @Email";
            using (var connection = new SqlConnection(_connectionString))
            {
                var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(query, new { Email = email });
                return usuario; // Retorna nulo se não encontrado, não lança exceção
            }
        }


        public async Task AtivarUsuarioAsync(int id)
        {
            var query = "UPDATE Usuarios_Dev SET Estado = 1 WHERE Id = @Id";
            await ExecuteQueryAsync(query, new { Id = id });
        }

        public async Task InativarUsuarioAsync(int id)
        {
            var query = "UPDATE Usuarios_Dev SET Estado = 0 WHERE Id = @Id";
            await ExecuteQueryAsync(query, new { Id = id });
        }


        private async Task ExecuteQueryAsync(string query, object parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> AlterarSenhaAsync(int id, string senhaAtual, string novaSenha)
        {
            var usuario = await GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return false; // Usuário não encontrado
            }

            // Verifica se a senha atual está correta
            if (!_hashService.VerifyPasswordHash(senhaAtual, usuario.SenhaHash))
            {
                return false; // Senha atual está incorreta, retorna false
            }

            // Atualiza a senha
            usuario.DefinirSenha(novaSenha, _hashService);
            var query = "UPDATE Usuarios_Dev SET SenhaHash = @SenhaHash WHERE Id = @Id";
            await ExecuteQueryAsync(query, new { Id = id, SenhaHash = usuario.SenhaHash });

            return true; // Senha alterada com sucesso
        }


    }
}