using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_7_4
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

        var Url = "startup_brasileira_lanca_aplicativo_inovador";
        
        var sinalGostou = 1;
        var tempoVisualizacao = 2.5;

        try
        {
            await NoticiaService.AtualizarEstatisticasVisualizacaoAsync(Url, sinalGostou, tempoVisualizacao);
            Console.WriteLine("Estatísticas atualizadas com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao atualizar as estatísticas:");
            Console.WriteLine(ex.Message);
        }
    }


}