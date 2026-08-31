using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_7_1
{
    public static async Task ExecutarAsync()
    {
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

        var contexto = new MongoContext(connectionString, "NoticiasDB");
        var NoticiaService = new NoticiaService(contexto);

        var url = "startup_brasileira_lanca_aplicativo_inovador";
        var novoTexto = "O aplicativo deles é top demais!";

        try
        {
            await NoticiaService.AtualizarTextoPorUrlAsync(url, novoTexto);
            Console.WriteLine("Texto da Notícia alterada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar o texto da Notícia:");
            Console.WriteLine(ex.Message);
        }
    }


}