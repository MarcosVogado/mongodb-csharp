using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_7_2
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

        var comentario = new ComentarioClass
        {
            Comentario = "Incrível ver jovens tão engajados com ciência!",
            Curtidas = 3,
            Usuario = "MariaSilva",
            Data = DateTime.UtcNow
        };

        try
        {
            await NoticiaService.AdicionarComentarioAsync(Url, comentario);
            Console.WriteLine("Comentário adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar adicionar o comentário da Notícia:");
            Console.WriteLine(ex.Message);
        }

    }


}