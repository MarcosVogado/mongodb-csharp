using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_8_1
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
            var (totalGostei, totalNaoGostei) = await NoticiaService.ObterReacoesNoPeriodoAsync(dataInicio, dataFim);
            Console.WriteLine($"Total de 'Gostei' : {totalGostei}");
            Console.WriteLine($"Total de 'Não Gostei' : {totalNaoGostei}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao obter os valores de Gostei e Não Gostei.");
            Console.WriteLine(ex.Message);
        }

    }


}