using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_8_3
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

        DateTime dataInicio = new DateTime(2025, 01, 01);
        DateTime dataFim = new DateTime(2025, 12, 31);

        try
        {
            var (maisAprovada, maisRejeitada) = await
            NoticiaService.ObterNoticiasMaisRelevantesAsync(dataInicio, dataFim);

            Console.WriteLine("Notícia com mais GOSTEI:");
            Console.WriteLine(maisAprovada ?? "Nenhuma encontrada.");

            Console.WriteLine("\nNotícia com mais NÃO GOSTEI:");
            Console.WriteLine(maisRejeitada ?? "Nenhuma encontrada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao obter as notícias mais relevantes:");
            Console.WriteLine(ex.Message);
        }

    }


}