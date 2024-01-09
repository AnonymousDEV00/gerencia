using GerenciaVendas.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GerenciaVendas.Services
{
    public class RelatorioService
    {
        private readonly string _connectionString;

        public RelatorioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<RelatorioVendasDto>> GerarRelatorioVendasAsync(DateTime dataInicio, DateTime dataFim, int? idVendedor = null)
        {
            var query = @"
                SELECT 
                    v.DataVenda, 
                    u.Nome AS NomeVendedor, 
                    p.Descricao AS DescricaoProduto, 
                    iv.Quantidade, 
                    iv.PrecoVenda, 
                    iv.Desconto, 
                    (iv.PrecoVenda - iv.Desconto) * iv.Quantidade AS TotalItem
                FROM Vendas_Dev v
                INNER JOIN Usuarios_Dev u ON v.IdUsuario = u.Id
                INNER JOIN ItensVenda_Dev iv ON v.Id = iv.IdVenda
                INNER JOIN Produtos_Dev p ON iv.IdProduto = p.Id
                WHERE v.DataVenda BETWEEN @DataInicio AND @DataFim
            ";

            if (idVendedor.HasValue)
            {
                query += "AND v.IdUsuario = @IdVendedor ";
            }

            query += "ORDER BY v.DataVenda DESC";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<RelatorioVendasDto>(query, new { DataInicio = dataInicio, DataFim = dataFim, IdVendedor = idVendedor });
            }
        }
    }

    public class RelatorioVendasDto
    {
        public DateTime DataVenda { get; set; }
        public string NomeVendedor { get; set; }
        public string DescricaoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Desconto { get; set; }
        public decimal TotalItem { get; set; }
    }
}
