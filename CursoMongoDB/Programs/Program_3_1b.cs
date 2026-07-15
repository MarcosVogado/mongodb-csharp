using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CursoMongoDB.Programs;

public static class Program_3_1b
{
    public static void Executar()
    {
        // Lê a connection string dos User Secrets (fora do repositório).
        // Configure com:
        //   dotnet user-secrets set "MongoDb:ConnectionString" "mongodb+srv://..."
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<SecretsAnchor>()
            .Build();

        var connectionString = configuration["MongoDb:ConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string não configurada. Rode: " +
                "dotnet user-secrets set \"MongoDb:ConnectionString\" \"<sua-string>\"");
        }

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("NoticiasDB");
        client.DropDatabase("NoticiasDB");

        Console.WriteLine("Banco de dados 'NoticiasDB' removido com sucesso!");
    }
}

