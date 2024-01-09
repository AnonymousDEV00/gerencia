using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GerenciaVendas.Services
{
    public class ItemVendaService
    {
        private readonly string _connectionString;

        public ItemVendaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ItemVenda>> GetAllItensVendaAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<ItemVenda>("SELECT * FROM ItensVenda_Dev");
            }
        }

        public async Task<ItemVenda> GetItemVendaByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<ItemVenda>(
                    "SELECT * FROM ItensVenda_Dev WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddItemVendaAsync(ItemVenda itemVenda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO ItensVenda_Dev (IdVenda, IdProduto, Quantidade, PrecoVenda, Desconto) VALUES (@IdVenda, @IdProduto, @Quantidade, @PrecoVenda, @Desconto)";
                await connection.ExecuteAsync(query, itemVenda);
            }
        }

        public async Task UpdateItemVendaAsync(ItemVenda itemVenda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE ItensVenda_Dev SET IdVenda = @IdVenda, IdProduto = @IdProduto, Quantidade = @Quantidade, PrecoVenda = @PrecoVenda, Desconto = @Desconto WHERE Id = @Id";
                await connection.ExecuteAsync(query, itemVenda);
            }
        }

        public async Task DeleteItemVendaAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("DELETE FROM ItensVenda_Dev WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
