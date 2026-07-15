using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CursoMongoDB.Programs;

public static class Program_3_2
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

        var Noticia = new NoticiaClass
        {
            Titulo = "Brasil bate Equador",
            Texto = "No Maracanã, Brasil vence o Equador por 2X0 ...",
            DataPublicacao = DateTime.Parse("2023-08-10T15:32:00Z"),
            Tags = new List<string> { "esporte", "brasil" },
            Jornalistas = new List<JornalistaClass>
            {
                new JornalistaClass { Nome = "Maria" }
            },
            Comentarios = new List<ComentarioClass>
            {
                new ComentarioClass
                {
                    Comentario = "Grande Jogo!",
                    Curtidas = 0,
                    Usuario = "Carlos",
                    Data = DateTime.Parse("2023-08-10T18:42:00Z")
                }
            },
            Anexos = new List<AnexoClass>
            {
                new AnexoClass
                {
                    NomeArquivo = "foto-jogo.jpg",
                    Url = "https://meusite.com.br/fotos/foto-jogo.jpg",
                    Tamanho = 204800,
                    Tipo = "imagem/jpg",
                    Cliques = 0
                }
            },
            Visualizacoes = 0,
            TotalComentarios = 1,
            Gostei = 0,
            NaoGostei = 0,
            TempoMedioLeitura = 0.0
        };

        var noticiaJson = Noticia.ToJson();

        try
        {

        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir o JSON como string:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Notícia no formato JSON:\n");
        Console.WriteLine(Noticia.ToJson());
    }


}