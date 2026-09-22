using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_8_2
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

        String Tag = "brasil";
        String Jornalista = "Fernando Torres";

        try
        {
            var (totalGostei, totalNaoGostei) = await NoticiaService.ObterReacoesPorFiltroAsync(dataInicio, dataFim, Jornalista, Tag);
            Console.WriteLine($"Total de 'Gostei': {totalGostei}");
            Console.WriteLine($"Total de 'Não Gostei': {totalNaoGostei}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro obter os valores de Gostei e Não Gostei:");
            Console.WriteLine(ex.Message);
        } 

    }


}