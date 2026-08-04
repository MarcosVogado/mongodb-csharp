using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_5_3b
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

        // var client = new MongoClient(connectionString);
        // var database = client.GetDatabase("NoticiasDB");
        // var collection = database.GetCollection<BsonDocument>("noticias");

        string caminhoArquivo = "C:\\tmp\\CursoMongoDB\\4905 - Vídeo 5.3 - Lista.js";

        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Arquivo noticias.json não encontrado!");
            return;
        }

        string conteudoJson = File.ReadAllText(caminhoArquivo);
        var listaNoticias = JsonConvert.DeserializeObject<List<NoticiaClass>>(conteudoJson);

        // var listaBson = new List<BsonDocument>();
        // foreach (var Noticia in listaNoticias)
        // {
        //     listaBson.Add(Noticia.ToBson());
        // }

        // try
        // {
        //     await collection.InsertManyAsync(listaBson);
        //     Console.WriteLine("Notícias incluídas com sucesso!");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine("Ocorreu um erro ao tentar inserir a lista de noticias:");
        //     Console.WriteLine(ex.Message);
        // }

        try
        {
            await NoticiaService.InserirNoticiasAsync(listaNoticias);
            Console.WriteLine("Notícias incluida com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir a lista de notícias:");
            Console.WriteLine(ex.Message);
        }

        
    }


}