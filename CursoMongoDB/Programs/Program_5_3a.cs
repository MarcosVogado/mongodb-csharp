using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using CursoMongoDB.Contexts;
using CursoMongoDB.Services;

namespace CursoMongoDB.Programs;

public static class Program_5_3a
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

        string jsonRecebido = @"{
            ""Titulo"": ""Festival de Música agita a cidade do Rio de Janeiro"",
            ""Texto"": ""O festival reuniu artistas nacionais e internacionais..."",
            ""DataPublicacao"": ""2024-11-22T20:30:00Z"",
            ""Tags"": [""cultura"", ""musica"", ""brasil""],
            ""Jornalistas"": [
                { ""Nome"": ""João Almeida"" }
            ],
            ""Comentarios"": [
                {
                    ""ComentarioTexto"": ""Evento sensacional!"",
                    ""Curtidas"": 19,
                    ""Usuario"": ""LarissaSantos"",
                    ""Data"": ""2024-11-22T22:10:00Z""
                }
            ],
            ""Anexos"": [
                {
                    ""NomeArquivo"": ""festival-musica.jpg"",
                    ""Url"": ""https://meusite.com/img/festival-musica.jpg"",
                    ""Tamanho"": 98000,
                    ""Tipo"": ""image/jpeg"",
                    ""Cliques"": 31
                }
            ],
            ""Visualizacoes"": 950,
            ""TotalComentarios"": 1,
            ""Gostei"": 76,
            ""NaoGostei"": 2,
            ""TempoMedioLeitura"": 3.1
        }";

        var Noticia = JsonConvert.DeserializeObject<NoticiaClass>(jsonRecebido);

        // var noticiaBson = Noticia.ToBson();

        // try
        // {
        //     await collection.InsertOneAsync(noticiaBson);
        //     Console.WriteLine("Notícia incluída com sucesso!");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine("Ocorreu um erro ao tentar inserir o JSON como string:");
        //     Console.WriteLine(ex.Message);
        // }

        try
        {
            await NoticiaService.InserirNoticiaAsync(Noticia);
            Console.WriteLine("Notícia incluida com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir o JSON como string:");
            Console.WriteLine(ex.Message);
        }
    }


}