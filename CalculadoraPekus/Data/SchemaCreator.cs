using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

public class SchemaCreator
{
    public static string DataBaseName = "calculadorapekus";
    public static void EnsureDatabaseAndTableExist(string _connectionString)
    {
        using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
        {
            dbConnection.Open();

            
            dbConnection.Execute($"CREATE DATABASE IF NOT EXISTS {DataBaseName};");

            
            dbConnection.ChangeDatabase(DataBaseName);

            
            dbConnection.Execute(@"
                CREATE TABLE IF NOT EXISTS `counts` (
                  `Id` int NOT NULL AUTO_INCREMENT,
                  `ValueA` double NOT NULL,
                  `ValueB` double NOT NULL,
                  `Operation` varchar(10) NOT NULL,
                  `Result` double NOT NULL,
                  `DateTime` datetime NOT NULL,
                  PRIMARY KEY (`Id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");
        }
    }
}
