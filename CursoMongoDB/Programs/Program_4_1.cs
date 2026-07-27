using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Json;

namespace CursoMongoDB.Programs;

public static class Program_4_1
{
    public static void Executar()
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

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("NoticiasDB");
        var collection = database.GetCollection<BsonDocument>("noticias");

        string jsonRecebido = @"{
            ""Titulo"": ""Brasil bate Equador"",
            ""Texto"": ""No Maracanã, Brasil vence por 2 a 0..."",
            ""DataPublicacao"": ""2023-08-10T15:32:00Z"",
            ""Tags"": [ ""esporte"", ""brasil""],
            ""Jornalistas"": [{ ""Nome"": ""Maria"" }],
            ""Comentarios"": [{
                ""ComentarioTexto"": ""Grande jogo!"",
                ""Curtidas"": 0,
                ""Usuario"": ""Carlos"",
                ""Data"": ""2023-08-10T18:45:00Z""
            }],
            ""Anexos"": [{
                ""NomeArquivo"": ""foto-jogo.jpg"",
                ""Url"": ""https://meusite.com/fotos/foto-jogo.jpg"",
                ""Tamanho"": 204800,
                ""Tipo"": ""imagem/jpeg"",
                ""Cliques"": 157
            }],
            ""Visualizacoes"": 0,
            ""TotalComentarios"": 1,
            ""Gostei"": 0,
            ""NaoGostei"": 0,
            ""TempoMedioLeitura"": 0.0
        }";

        var noticiaBson = Noticia.ToBson();

        try
        {
            collection.InsertOne(noticiaBson);
            Console.WriteLine("Notícia incluída com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir o JSON como string:");
            Console.WriteLine(ex.Message);
        }
    }


}