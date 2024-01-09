using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GerenciaVendas.Services
{
    public class ProdutoService
    {
        private readonly string _connectionString;

        public ProdutoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // CRUD com Ativação e Inativação

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Produto>("SELECT * FROM Produtos_Dev");
            }
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Produto>(
                    "SELECT * FROM Produtos_Dev WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Produtos_Dev (Descricao, Preco, QuantidadeEstoque, DataCadastro, Estado) VALUES (@Descricao, @Preco, @QuantidadeEstoque, GETDATE(), 1)";
                await connection.ExecuteAsync(query, produto);
            }
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Produtos_Dev SET Descricao = @Descricao, Preco = @Preco, QuantidadeEstoque = @QuantidadeEstoque, Estado = @Estado WHERE Id = @Id";
                await connection.ExecuteAsync(query, produto);
            }
        }

        public async Task InativarProdutoAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("UPDATE Produtos_Dev SET Estado = 0 WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AtivarProdutoAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("UPDATE Produtos_Dev SET Estado = 1 WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
