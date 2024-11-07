using CalculadoraPekus.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace CalculadoraPekus.Controllers

{
    public class CalculadoraController : Controller
    {
        private readonly string _connection;

        public CalculadoraController(IConfiguration configuration)
        {
            _connection = $"{configuration.GetConnectionString("DefaultConnection")};Database={SchemaCreator.DataBaseName};";
        }

        // Ação para carregar a página inicial
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Ação para calcular o resultado da operação
        [HttpPost]
        public IActionResult Calculate(double valueA, double valueB, string operation)
        {
            double result;

            // Verifica a operação e calcula o resultado
            switch (operation)
            {
                case "+":
                    result = valueA + valueB;
                    break;
                case "-":
                    result = valueA - valueB;
                    break;
                case "*":
                    result = valueA * valueB;
                    break;
                case "/":
                    if (valueB == 0)
                        return Content("Erro: Divisão por zero.");
                    result = valueA / valueB;
                    break;
                default:
                    return Content("Operação inválida.");
            }

            // Cria um novo objeto para salvar no banco de dados
            var calculo = new CalculadoraViewModel
            {
                ValueA = valueA,
                ValueB = valueB,
                Operation = operation,
                Result = result,
                DateTime = DateTime.Now
            };

            // Salva o cálculo no banco de dados usando Dapper
            using (IDbConnection dbConnection = new MySqlConnection(_connection))
            {
                dbConnection.Open();
                
                var query = "INSERT INTO counts (ValueA, ValueB, Operation, Result, DateTime) VALUES (@ValorA, @ValorB, @Operacao, @Resultado, @DataHora)";
                
                dbConnection.Execute(query, new
                {
                    ValorA = calculo.ValueA,
                    ValorB = calculo.ValueB,
                    Operacao = calculo.Operation,
                    Resultado = calculo.Result,
                    DataHora = calculo.DateTime
                });
            }

            // Mensagem de sucesso
            TempData["SuccessMessage"] = "Cálculo armazenado com sucesso.";
            return RedirectToAction("Index");
        }

        // Ação para mostrar o histórico de cálculos
        [HttpGet]
        public IActionResult History()
        {
            // Recupera o histórico de cálculos
            using (IDbConnection dbConnection = new MySqlConnection(_connection))
            {
                dbConnection.Open();
                var query = "SELECT * FROM counts ORDER BY DateTime DESC";
                var calculations = dbConnection.Query<CalculadoraViewModel>(query).ToList();
                return View(calculations);
            }
        }
    }
}
