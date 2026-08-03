using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace CursoMongoDB.Programs
{
    public static class Program_4_4a
    {
        public static void Executar()
        {
            Console.WriteLine("Processo iniciado");
            System.Threading.Thread.Sleep(15000);
            Console.WriteLine("Processo síncrono finalizado");
            
        }
    }
}