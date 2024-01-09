using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;

namespace GerenciaVendas.Services
{
    public class VendaService
    {
        private readonly string _connectionString;

        public VendaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Venda>> GetAllVendasAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryAsync<Venda>("SELECT * FROM Vendas_Dev");
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        public async Task<Venda> GetVendaByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return await connection.QuerySingleOrDefaultAsync<Venda>(
                        "SELECT * FROM Vendas_Dev WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        public async Task AddVendaAsync(Venda venda)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO Vendas_Dev (IdUsuario, DataVenda, TotalVenda) VALUES (@IdUsuario, GETDATE(), @TotalVenda)";
                    await connection.ExecuteAsync(query, venda);
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        public async Task UpdateVendaAsync(Venda venda)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Vendas_Dev SET IdUsuario = @IdUsuario, TotalVenda = @TotalVenda WHERE Id = @Id";
                    await connection.ExecuteAsync(query, venda);
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        public async Task InativarVendaAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Vendas_Dev SET Estado = 0 WHERE Id = @Id";
                    await connection.ExecuteAsync(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        public async Task AtivarVendaAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE Vendas_Dev SET Estado = 1 WHERE Id = @Id";
                    await connection.ExecuteAsync(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                // Log e tratamento de erros
                throw;
            }
        }

        // Outros métodos conforme necessário
    }
}
