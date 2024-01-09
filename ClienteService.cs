using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GerenciaVendas.Services
{
    public class ClienteService
    {
        private readonly string _connectionString;

        public ClienteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // CRUD com Ativação e Inativação

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Cliente>("SELECT * FROM Clientes_Dev");
            }
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Cliente>(
                    "SELECT * FROM Clientes_Dev WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Clientes_Dev (Nome, DataNascimento, CPF_CNPJ, Endereco, DataCadastro, Estado) VALUES (@Nome, @DataNascimento, @CPF_CNPJ, @Endereco, GETDATE(), 1)";
                await connection.ExecuteAsync(query, cliente);
            }
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Clientes_Dev SET Nome = @Nome, DataNascimento = @DataNascimento, CPF_CNPJ = @CPF_CNPJ, Endereco = @Endereco, Estado = @Estado WHERE Id = @Id";
                await connection.ExecuteAsync(query, cliente);
            }
        }

        public async Task InativarClienteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("UPDATE Clientes_Dev SET Estado = 0 WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AtivarClienteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync("UPDATE Clientes_Dev SET Estado = 1 WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
